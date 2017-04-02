//Author: Evan Schober
//CIS 237
//Assignment 5
/*
 * The Menu Choices Displayed By The UI
 * 1. Print The Entire List Of Items
 * 2. Search For An Item
 * 3. Add New Item To The List
 * 4. Update An Existing Item
 * 5. Remove An Item From The List
 * 6. Exit Program
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferHeight = 5000;

            BeverageESchoberEntities beveragesESchoberEntities = new BeverageESchoberEntities();

            //Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            //Create an instance of the WineItemCollection class
            BeverageCollection beverageCollection = new BeverageCollection(beveragesESchoberEntities);

            //Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            //Display the Menu and get the response. Store the response in the choice integer
            //This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            while (choice != 6)
            {
                switch (choice)
                {
                    case 1:
                        //Display all of the items
                        userInterface.DisplayAllItems(beverageCollection.GetPrintStringsForAllItems());
                        break;

                    case 2:
                        //Search For An Item
                        string searchQuery = userInterface.GetSearchQuery();
                        string itemInformation = beverageCollection.FindById(searchQuery);
                        if (itemInformation != null)
                        {
                            userInterface.DisplayItemFound(itemInformation);
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;

                    case 3:
                        //Add A New Item To The List
                        string[] newItemInformation = userInterface.GetNewItemInformation();
                        if (beverageCollection.FindById(newItemInformation[0]) == null)
                        {
                            beverageCollection.AddNewItem(newItemInformation[0], newItemInformation[1], newItemInformation[2]);
                            userInterface.DisplayAddWineItemSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemAlreadyExistsError();
                        }
                        break;
                    case 4:
                        //Update an existing item
                        searchQuery = userInterface.GetSearchQuery();
                        itemInformation = beverageCollection.FindById(searchQuery);
                        if (itemInformation != null)
                        {
                            userInterface.DisplayItemFound(itemInformation);
                            beverageCollection.UpdateItem(userInterface.UpdateItemInformation(searchQuery));
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;
                    case 5:
                        //Remove an item from the list
                        searchQuery = userInterface.GetSearchQuery();
                        itemInformation = beverageCollection.FindById(searchQuery);
                        if (itemInformation != null)
                        {
                            userInterface.DisplayItemFound(itemInformation);
                            beverageCollection.RemoveItem(searchQuery);
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;
                }

                //Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }

        }
    }
}
