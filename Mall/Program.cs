//***************************************************************************************************
//Program Title: Mall Management System
//Programmer: Jackson Meyn (22558031)
//Version: 0.2
//Decription: Create and manage malls and the array of stores within a mall 
//through use of a Mall class, and PermanentStore and NonPermanentStore classes.
//Version 0.2 also incorporates polymorphism and inheritance concepts
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall
{
    //Interfaces
    public interface IStore
    {
        bool IsOccupied();
        String StoreCategory();
    }

    //End Interfaces

    //Classes
    class Program
    {
        static void Main(string[] args)
        {
            //Test the default constructor for mall
            Mall testMall = new Mall();

            //Add a new non permanent store with arguments
            testMall.Add("Test Store","Food",25.2f,23.34f,true, new DateTime(2018, 1, 29));
            //testMall.Add();

            ////Add a new permanent store with arguments
            testMall.Add("Test Store 2","Children's Apparel",15.23f,17.1f,false,4.2f);
            //testMall.Add();

            ////Print a stores list
            testMall.Print();

            //Check Test Store category
            Console.WriteLine("Testing valid index and correct category in check function below...");
            Console.WriteLine(testMall.Check(0, "Food"));
            Console.WriteLine("Testing invalid index in check function below...");
            Console.WriteLine(testMall.Check(7, "Food"));
            Console.WriteLine("Testing wrong category in check function below...");
            Console.WriteLine(testMall.Check(0, "Travel Accessories"));
            ////Edit one store
            testMall.editStore("Test Store");
            ////Delete one store
            testMall.deleteStore("Test Store 2");

            ////Display stores list again to see changes made
            testMall.Print();

            //Test monthly maintenance fee method
            testMall.CalculateMonthlyMaintenanceFee();

            //Hold the screen
            Console.ReadLine();
        }
    }

    class Mall
    {
        //Fields
        private string name;
        private const int MAX_STORES = 100;
        private Store[] storesArray = new Store[MAX_STORES];

        //Properties
        public string Name { get => name; set => name = value; }

        //Constructors
        public Mall()
        {
            while (Name == null || Name == "")
            {
                Console.WriteLine("Please enter a name for the new mall. The mall name cannot be blank.");
                Name = Console.ReadLine();
            }
            
        }

        public Mall(string mallName)
        {
            Name = mallName;
        }
        //End constructors

        //Methods
        public void Add(string storeName, string category, float length, float width, bool occupied, float height)
        {
            bool storeAdded = false;
            //Make sure there's no store with that name already
            for (int i = 0; i < MAX_STORES; i++)
            {
                        if (storesArray[i] != null && storesArray[i].StoreName == storeName)
                        {
                            Console.WriteLine("Store not created. A store with the name " + storeName + "already exists.");
                            return;
                        }
            }

            //Make sure there's at least one empty space
            for (int i = 0; i < MAX_STORES; i++)
            {
                if (storesArray[i] == null)
                {
                    storesArray[i] = new PermanentStore(storeName, category, length, width, occupied, height);
                    storeAdded = true;
                    break;
                }
            }
            if (storeAdded == false)
            {
                Console.WriteLine("A new permanent store cannot be added because this mall is full. Consider deleting a permanent store then trying again");
            }


        }

        public void Add()
        {
            Console.WriteLine("Enter p to add a permanent store, or n to add a non permanent store");
            string input = Console.ReadLine();
            while (input != "n" && input != "N" && input != "p" && input != "P")
            {
                Console.WriteLine("Oops! please only enter p or n.");
                Console.WriteLine("Enter p to add a permanent store, or n to add a non permanent store");
                input = Console.ReadLine();
            }

            if (input == "p" || input == "P")
            {
                Console.WriteLine("You have chosen to create a new permanent store in mall " + Name);
                bool storeAdded = false;
                //Make sure there's at least one empty space
                for (int i = 0; i < MAX_STORES; i++)
                {
                    if (storesArray[i] == null)
                    {
                        storesArray[i] = new PermanentStore();
                        storeAdded = true;
                        break;
                    }
                }
                if (storeAdded == false)
                {
                    Console.WriteLine("A new permanent store cannot be added because this mall is full. Consider deleting a permanent store then trying again");
                }
            } else
            {
                Console.WriteLine("You have chosen to create a new non-permanent store in mall " + Name);
                bool storeAdded = false;
                //Make sure there's at least one empty space
                for (int i = 0; i < MAX_STORES; i++)
                {
                    if (storesArray[i] == null)
                    {
                        storesArray[i] = new NonPermanentStore();
                        storeAdded = true;
                        break;
                    }
                }
                if (storeAdded == false)
                {
                    Console.WriteLine("A new non-permanent store cannot be added because this mall is full. Consider deleting a non-permanent store then trying again");
                }
            }
            


        }

        public void Add(string storeName, string category, float length, float width, bool occupied, DateTime expirationDate)
        {
            bool storeAdded = false;
            //Make sure there's no store with that name already
            for (int i = 0; i < MAX_STORES; i++)
            {
                if (storesArray[i] != null)
                {
                    if (storesArray[i].StoreName == storeName)
                    {
                        Console.WriteLine("Store not created. A non-permanent store with the name " + storeName + "already exists.");
                        return;
                    }

                }
            }

            //Find empty space to insert the store
            for (int i = 0; i < MAX_STORES; i++)
            {
                if (storesArray[i] == null)
                {
                    storesArray[i] = new NonPermanentStore(storeName, category, length, width, occupied, expirationDate);
                    storeAdded = true;
                    break;
                }
            }
            if (storeAdded == false)
            {
                Console.WriteLine("A new non-permanent store cannot be added because this mall is full. Consider deleting a non-permanent store then trying again");
            }


        }

        public void Print()
        {
            bool storePrinted = false;
            Console.WriteLine("Below is a list of stores in mall " + this.name);
            Console.WriteLine("================================================");
            foreach (Store s in storesArray)
            {
                if (s != null)
                {
                    Console.WriteLine("Store name: " + s.StoreName);
                    Console.WriteLine("Category: " + s.StoreCategory());
                    Console.Write("Occupied: ");
                    if (s.IsOccupied())
                    {
                        Console.WriteLine("yes");
                    } else
                    {
                        Console.WriteLine("no");
                    }
                    
                    if (s is PermanentStore)
                    {
                        PermanentStore temp = (PermanentStore)s;
                        Console.WriteLine("Monthly maintenance fee: " + String.Format("{0:C2}", temp.CalculateMonthlyMaintenanceFee()));
                        Console.WriteLine("Store type: permanent");
                    } else
                    {
                        NonPermanentStore temp = (NonPermanentStore)s;
                        Console.WriteLine("Monthly maintenance fee: " + String.Format("{0:C2}", temp.CalculateMonthlyMaintenanceFee()));
                        Console.WriteLine("Store type: non-permanent");
                    }
                    Console.WriteLine();
                    storePrinted = true;
                }


            }

            if (!storePrinted)
            {
                Console.WriteLine("Currently no stores to display");
            }

            Console.WriteLine("================================================");
        }

        public bool Check(int index, string category)
        {
            try
            {
                if (storesArray[index].StoreCategory() == category)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("There is no store at index {0} to check.", index);
                return false;
            }
        }

        public void deleteStore(string storeName)
        {
            bool storeFound = false;
            //Find the store to delete
            for (int i = 0; i < MAX_STORES; i++)
            {
                if (storesArray[i] != null)
                {
                    if (storesArray[i].StoreName == storeName)
                    {
                        storesArray[i] = null;
                        storeFound = true;
                        break;
                    }
                }
                
            }

            //Check permanent stores if not already found
            if (storeFound == false) {

                for (int i = 0; i < MAX_STORES; i++)
                {
                    if (storesArray[i] != null)
                    {
                        if (storesArray[i].StoreName == storeName)
                        {
                            storesArray[i] = null;
                            storeFound = true;
                            break;
                        }
                    }
                }

            }

            //Report result
            if (storeFound == true)
            {
                Console.WriteLine("Successfully deleted " + storeName);
            } else
            {
                Console.WriteLine("Could not find store named " + storeName + " to delete.");
            }


        }

        public void editStore(string storeName)
        {
            bool storeFound = false;
            int storeIndex = -1;
            //Find the store to edit
            for (int i = 0; i < MAX_STORES; i++)
            {
                //Begin the editing
                if (storesArray[i] != null && storesArray[i].StoreName == storeName)
                {
                    storeFound = true;
                    storeIndex = i;
                    
                        
                    Console.WriteLine("You've chosen to edit the store named " + storesArray[i].StoreName);
                    Console.WriteLine("Enter a new name for the store or simply press Enter to leave the store's name the same.");
                    string input = Console.ReadLine();
                    if (input != "")
                    {
                        storesArray[i].StoreName = input;
                    }

                    Console.WriteLine("The store has a category name of " + storesArray[i].StoreCategory());
                    Console.WriteLine("Enter a new category for the store or simply press Enter to leave the store's name the same.");
                    input = Console.ReadLine();
                    if (input != "")
                    {
                        storesArray[i].Category = input;
                    }

                    Console.WriteLine("The store currently has a length in metres of " + storesArray[i].Length);
                    Console.WriteLine("Enter a new length for the store or simply press Enter to leave the store's length the same.");
                    input = Console.ReadLine();
                    if (input != "")
                    {
                        bool parameterChanged = false;
                        while (!parameterChanged)
                        {
                            try
                            {
                                storesArray[i].Length = Convert.ToSingle(input);
                                parameterChanged = true;
                            } catch (FormatException e) {
                                Console.WriteLine(e.Message);
                            }
                        }
                        
                        
                    }

                    Console.WriteLine("The store currently has a width in metres of " + storesArray[i].Width);
                    Console.WriteLine("Enter a new width for the store or simply press Enter to leave the store's width the same.");
                    input = Console.ReadLine();
                    if (input != "")
                    {
                        bool parameterChanged = false;
                        while (!parameterChanged)
                        {
                            try
                            {
                                storesArray[i].Width = Convert.ToSingle(input);
                                parameterChanged = true;
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        
                    }

                    Console.Write("The store is currently ");
                    if (storesArray[i].Occupied == true)
                    {
                        Console.WriteLine("occupied");
                    } else
                    {
                        Console.WriteLine("unoccupied");
                    }
                    Console.WriteLine("Enter y if the store is occupied, n if it is not, or simply press Enter to leave the store's occupation status the same.");
                    bool validResponse = false;
                    input = Console.ReadLine();
                    if (input != "")
                    {
                        while (validResponse == false)
                        {
                            if (input == "y" || input == "Y")
                            {
                                storesArray[i].Occupied = true;
                                validResponse = true;

                            }
                            else if (input == "n" || input == "N")
                            {
                                storesArray[i].Occupied = false;
                                validResponse = true;
                            }
                            else
                            {
                                Console.WriteLine("Oops! Please only enter y if the store is occupied, or n if it is not.");
                                input = Console.ReadLine();
                            }
                        }


                    }
                    else
                    {
                        validResponse = true;
                    }


                    if (storesArray[i] is PermanentStore)
                    {
                        PermanentStore tempStore = (PermanentStore)storesArray[i];
                        Console.WriteLine("The store currently has a height in metres of" + tempStore.Height);
                        Console.WriteLine("Enter a new height for the store or simply press Enter to leave the store's height the same.");
                        input = Console.ReadLine();
                        if (input != "")
                        {
                            bool parameterChanged = false;
                            while (!parameterChanged)
                            {
                                try
                                {
                                    tempStore.Height = Convert.ToSingle(Console.ReadLine());
                                    parameterChanged = true;
                                }
                                catch (FormatException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }

                        }
                        storesArray[i] = tempStore;
                    }
                    else
                    {
                        NonPermanentStore tempStore = (NonPermanentStore)storesArray[i];
                        Console.WriteLine("The store currently has an expiry date of " + tempStore.ExpirationDate);
                        Console.WriteLine("Enter the YEAR of the new expiration date for the store or simply press Enter to leave the store's expiry date the same.");
                        input = Console.ReadLine();
                        if (input != "")
                        {
                            bool parameterChanged = false;
                            while (!parameterChanged)
                            {
                                try
                                {
                                    int yearInput = Convert.ToInt16(input);
                                    while (yearInput < 2017 || yearInput > 2100)
                                    {
                                        Console.WriteLine("Enter a year between 2017 and 2100");
                                        yearInput = Convert.ToInt16(Console.ReadLine());
                                    }
                                    Console.WriteLine("Now enter the month of the new expiry date (1-12)");
                                    int monthInput = Convert.ToInt16(Console.ReadLine());
                                    while (monthInput < 1 || monthInput > 12)
                                    {
                                        Console.WriteLine("Enter a month between 1 and 12");
                                        monthInput = Convert.ToInt16(Console.ReadLine());
                                    }


                                    Console.WriteLine("Enter the day of the new expiry date (1-31)");
                                    int dayInput = Convert.ToInt16(Console.ReadLine());
                                    while (dayInput < 1 || dayInput > 31)
                                    {
                                        Console.WriteLine("Enter a day between 1 and 31");
                                        dayInput = Convert.ToInt16(Console.ReadLine());
                                    }

                                    tempStore.ExpirationDate = new DateTime(yearInput, monthInput, dayInput);
                                    parameterChanged = true;
                                }
                                catch (FormatException e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Beginning the date entry sequence again...");
                                }
                                catch (ArgumentOutOfRangeException e)
                                {
                                    Console.WriteLine("The date entered is invalid. Please try again");
                                }
                            }
                        }

                        storesArray[i] = tempStore;
                    }

                    
                    Console.WriteLine("Store successfully edited");
                    
                    break;
                }
            }


            if (storeFound == false)
            {
                Console.WriteLine("Could not find a store with the name " + storeName + " to edit");
            }
            

        }

        public float CalculateMonthlyMaintenanceFee()
        {
            float permanentTotal = 0;
            float nonPermanentTotal = 0;
            float grandTotal = 0;

            foreach (Store s in storesArray)
            {

                if (s != null)
                {
                    float storeFee = s.CalculateMonthlyMaintenanceFee();
                    nonPermanentTotal += storeFee;
                }
                else if (s != null && s is PermanentStore)
                {
                    PermanentStore ps = (PermanentStore)s;
                    float storeFee = ps.CalculateMonthlyMaintenanceFee();
                    permanentTotal += storeFee;
                }

            }

            grandTotal = nonPermanentTotal + permanentTotal;
            Console.WriteLine("MONTHLY MAINTENANCE FEE");
            Console.WriteLine("==============================================");
            Console.WriteLine("The monthly maintenance fee for mall " + this.Name + " is:");
            Console.WriteLine("Non permanent stores: " + String.Format("{0:C2}", nonPermanentTotal));
            Console.WriteLine("Permanent stores: " + String.Format("{0:C2}", permanentTotal));
            Console.WriteLine("Grand Total: " + String.Format("{0:C2}", grandTotal));
            Console.WriteLine("==============================================");

            return grandTotal;
        }
        //End methods
    }

    abstract class Store : IStore
    {
        //Fields
        private string storeName;
        private float length;
        private float width;
        private bool occupied;
        private string category;

        //Properties
        public string StoreName { get => storeName; set => storeName = value; }
        public float Length { get => length; set => length = value; }
        public float Width { get => width; set => width = value; }
        public bool Occupied { get => occupied; set => occupied = value; }
        public string Category { get => category; set => category = value; }

        //Constructors
        public Store()
        {
            while (StoreName == null || StoreName == "")
            {
                Console.WriteLine("Please enter a name for the store. A store's name cannot be blank.");
                StoreName = Console.ReadLine();
            }

            while (Category == null || Category == "")
            {
                Console.WriteLine("Please enter the store's category e.g. General. A store's category cannot be blank.");
                Category = Console.ReadLine();
            }
            bool valid = false;
            while (valid == false)
            {
                try
                {
                    Console.WriteLine("Enter the length of the store in metres");
                    Length = Convert.ToSingle(Console.ReadLine());
                    valid = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }

            valid = false;

            while (valid == false)
            {
                try
                {
                    Console.WriteLine("Enter the width of the store in metres");
                    Width = Convert.ToSingle(Console.ReadLine());
                    valid = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            Console.WriteLine("Enter y if the store is currently occupied, or just press Enter if it is not");
            string input = Console.ReadLine();

            if (input == "Y" || input == "y")
            {
                Occupied = true;
            } else
            {
                Occupied = false;
                Console.WriteLine("The store is unoccupied.");
            }

        }

        public Store(string storeName, string category, float length, float width, bool occupied)
        {
            StoreName = storeName;
            Category = category;
            Length = length;
            Width = width;
            Occupied = occupied;
        }
        //End constructors

        public bool IsOccupied()
        {
            return Occupied;
        }

        public string StoreCategory()
        {
            return Category;
        }

        public abstract float CalculateMonthlyMaintenanceFee();
        
    }

    class NonPermanentStore : Store
    {
        //Fields
        private DateTime expirationDate;
        private const int NON_PERMANENT_STORE_COST = 70;

        //Properties
        public DateTime ExpirationDate { get => expirationDate; set => expirationDate = value; }

        //Constructors
        public NonPermanentStore()
        {
            bool validDate = false;
            while (!validDate)
            {
                try
                {
                    

                    Console.WriteLine("Enter the year the store lease expires in");
                    int yearInput = Convert.ToInt16(Console.ReadLine());
                    while (yearInput < 2017 || yearInput > 2100)
                    {
                        Console.WriteLine("Enter a year between 2017 and 2100");
                        yearInput = Convert.ToInt16(Console.ReadLine());
                    }
                    Console.WriteLine("Enter the number corresponding to the month the lease expires in (1-12)");
                    int monthInput = Convert.ToInt16(Console.ReadLine());
                    while (monthInput < 1 || monthInput > 12)
                    {
                        Console.WriteLine("Enter a month between 1 and 12");
                        monthInput = Convert.ToInt16(Console.ReadLine());
                    }


                    Console.WriteLine("Enter the day of the month the lease expires (1-31)");
                    int dayInput = Convert.ToInt16(Console.ReadLine());
                    while (dayInput < 1 || dayInput > 31)
                    {
                        Console.WriteLine("Enter a day between 1 and 31");
                        dayInput = Convert.ToInt16(Console.ReadLine());
                    }

                    ExpirationDate = new DateTime(yearInput, monthInput, dayInput);
                    validDate = true;

                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Beginning the date entry sequence again...");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("The date appears to be invalid. Please try again.");
                }
            }
            
            
        }

        public NonPermanentStore(string storeName, string category, float length, float width, bool occupied, DateTime expirationDate) : base(storeName, category, length, width, occupied)
        {
            ExpirationDate = expirationDate;
        }

        public override float CalculateMonthlyMaintenanceFee()
        {
            float fee = (Length * Width) * NON_PERMANENT_STORE_COST;
            return fee;
        }
    }

    class PermanentStore : Store
    {
        //Fields
        private float height;
        private const int PERMANENT_STORE_COST = 150;

        //Properties
        public float Height { get => height; set => height = value; }

        //Constructors
        public PermanentStore()
        {
            bool valid = false;
            while (!valid)
            {
                try
                {
                    Console.WriteLine("Enter the height of the store in metres.");
                    Height = Convert.ToSingle(Console.ReadLine());
                    valid = true;
                } catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
            
        }

        public PermanentStore(string storeName, string category, float length, float width, bool occupied, float height) : base(storeName, category, length, width, occupied)
        {
            Height = height;
        }
        //End constructors

        public override float CalculateMonthlyMaintenanceFee()
        {
            float fee = (Length * Width * Height) * PERMANENT_STORE_COST;
            return fee;
        }
        
    }
    //End classes
}
