//Author: Evan Schober
//CIS 237
//Assignment 5
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment5
{
    class BeverageCollection
    {
        //Private Variables
        BeverageESchoberEntities beveragesESchoberEntities;
        int wineItemsLength = 0;

        //Constuctor. Must pass the size of the collection.
        public BeverageCollection(BeverageESchoberEntities beveragesESchoberEntities)
        {
            this.beveragesESchoberEntities = beveragesESchoberEntities;
            foreach(Beverage beverage in beveragesESchoberEntities.Beverages)
            {
                wineItemsLength++;
            }
        }

        //Add a new item to the collection
        public void AddNewItem(string id, string description, string pack)
        {
            Beverage newBeverageToAdd = new Beverage();
            //Add a new Item to the database, save the state of the database and increment the Length variable.
            newBeverageToAdd.id = id;
            newBeverageToAdd.name = description;
            newBeverageToAdd.pack = pack;

            try
            {
                //Add a new item to the collection
                beveragesESchoberEntities.Beverages.Add(newBeverageToAdd);

                beveragesESchoberEntities.SaveChanges();
            }
            catch(Exception e)
            {
                //Remove the new beverage from the collection since it can't be saved
                beveragesESchoberEntities.Beverages.Remove(newBeverageToAdd);

                Console.WriteLine("Can't add the record. An entry already exists with that primary key.");
            }

            wineItemsLength++;
        }

        //Update an existing item in the collection
        public void UpdateItem(string[] updateItem)
        {
            Beverage beverageToUpdate = new Beverage();
            //Add a new Item to the database, save the state of the database and increment the Length variable.
            try
            {
                beverageToUpdate = beveragesESchoberEntities.Beverages.Where(beverage => beverage.id == updateItem[0]).First();

                beverageToUpdate.id = updateItem[0];
                beverageToUpdate.name = updateItem[1];
                beverageToUpdate.pack = updateItem[2];

                beveragesESchoberEntities.SaveChanges();
            }
            catch (Exception e)
            {
                //Exception message is sufficient. No further output/action needed
            }
        }

        public void RemoveItem(string id)
        {
            Beverage beverageToRemove = beveragesESchoberEntities.Beverages.Find(id);
            //Remove Item from the database, save the state of the database and decrement the Lenghth variable.

            beveragesESchoberEntities.Beverages.Remove(beverageToRemove);

            beveragesESchoberEntities.SaveChanges();

            wineItemsLength--;
        }
        
        //Get The Print String Array For All Items
        public string[] GetPrintStringsForAllItems()
        {
            //Create and array to hold all of the printed strings
            string[] allItemStrings = new string[wineItemsLength];
            //set a counter to be used
            int counter = 0;

            //For each item in the collection
            foreach (Beverage beverage in beveragesESchoberEntities.Beverages)
            {
                //Add the results of calling ToString on the item to the string array.
                allItemStrings[counter] = beverage.id + " " + beverage.name + " " + beverage.pack;
                counter++;
            }
            //Return the array of item strings
            return allItemStrings;
        }

        //Find an item by it's Id
        public string FindById(string id)
        {
            //Declare return string for the possible found item
            string returnString = null;
            try
            {
                Beverage beverageToFind = beveragesESchoberEntities.Beverages.Where(beverage => beverage.id == id).First();

                returnString = beverageToFind.id + " " + beverageToFind.name + " " + beverageToFind.pack;
            }
            catch(Exception e)
            {
                //Exception message is sufficient. No further output/action needed
            }

            //Return the returnString
            return returnString;
        }

    }
}
