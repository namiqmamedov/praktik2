using Core.Constants;
using Core.Helper;
using Manage.Controller;
using System;

public class Program
{
    static void Main()
    {
        OwnerController _ownerController = new OwnerController();
        DrugStoreController _drugStoreController = new DrugStoreController();

        while (true)
        {
            ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "1 - Create Owner");
            ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "2 - Update Owner");
            ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "3 - Delete Owner");
            ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "4 - Get All Owner");
            Console.WriteLine("-------");
            ConsoleHelper.WriteTextWithColor(ConsoleColor.Gray, "Select Options");
            string number = Console.ReadLine();

            int SelectedNumber;
            bool result = int.TryParse(number, out SelectedNumber);


            if (result)
            {
                if (SelectedNumber >= 0 && SelectedNumber <= 10)
                {
                    switch (SelectedNumber)
                    {
                        case (int)Options.CreateOwner:
                            _ownerController.CreateOwner();
                            break;
                        case (int)Options.UpdateOwner:
                            _ownerController.Update();
                            break;
                        case (int)Options.DeleteOwner:
                            _ownerController.Delete();
                            break;
                        case (int)Options.GetAllOwner:
                            _ownerController.GetAll();
                            break;
                    }
                }
                else
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Please, enter correct number!");
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Please enter correct number!");
            }
        }
    }
}

