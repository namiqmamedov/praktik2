using Core.Entities;
using Core.Helper;
using DataAccess.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Controller
{
    public class DrugStoreController
    {
        private DrugStoreRepository _drugStoreRepository;
        private OwnerRepository _ownerRepository;
        public DrugStoreController()
        {
            _drugStoreRepository = new DrugStoreRepository();
            _ownerRepository = new OwnerRepository();
        }

        public void Create()
        {

            var owners = _ownerRepository.GetAll();
            if (owners.Count != 0)
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter drugstore name");
                string name = Console.ReadLine();

                ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter drugstore address");
                string address = Console.ReadLine();

                ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter drugstore contact number");
                string number = Console.ReadLine();
                int contactNumber;
                bool result = int.TryParse(number, out contactNumber);

                ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, "All owners");
                foreach (var owner in owners)
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, $"ID - {owner.ID} Fullname - {owner.Name} {owner.Surname}");
                }
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter owner name:");
                string ownerName = Console.ReadLine();

                var dbOwner = _ownerRepository.Get(o => o.Name.ToLower() == ownerName.ToLower());


                if (dbOwner != null)
                {
                    var drugStore = new DrugStore
                    {
                        Name = name,
                        Address = address,
                        //ContactNumber = contactNumber,
                    };

                    _drugStoreRepository.Create(drugStore);
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Green, $"Name:{drugStore.Name}, Address:{drugStore.Address}, ContactNumber:{drugStore.ContactNumber}, Owner:{drugStore.Owner.Name}");
                }
                else
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, $"Including owner doesn't exist");
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, $"You must create owner before creating of drugstore");
            }


        }

        public void Update()
        {
            var owners = _ownerRepository.GetAll();
            if (owners.Count > 0)
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Blue, "All owner list");
                foreach (var owner in owners)
                {

                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, $"ID - {owner.ID}, Fullname - {owner.Name} {owner.Surname}");
                }
                ID:  ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter owner ID");
                string id = Console.ReadLine();
                int ownerID;
                var result = int.TryParse(id, out ownerID);
                if (result)
                {
                    var dbDrugStore = _drugStoreRepository.Get(d => d.ID == ownerID);
                    if (dbDrugStore != null)
                    {
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "Enter drugstore new name");
                        string Name = Console.ReadLine();

                        ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "Enter drugstore new address");
                        string Address = Console.ReadLine();

                    ContactNumber: ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "Enter drugstore new contact number");
                        string Number = Console.ReadLine();
                        int contactNumber;
                        result = int.TryParse(Number, out contactNumber);
                        if (result)
                        {
                            var updatedStore = new DrugStore
                            {
                                Name = Name,
                                Address = Address,
                                //ContactNumber = contactNumber,
                            };
                            _drugStoreRepository.Update(updatedStore);
                            ConsoleHelper.WriteTextWithColor(ConsoleColor.Green, $"{updatedStore.Name} {updatedStore.Address} is updated to successfully");
                        }
                        else
                        {
                            ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Please, enter number in correct format");
                            goto ContactNumber;
                        }
                    }
                    else
                    {
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Owner doesn't exist with this ID");
                        goto ID;
                    }
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "There are not any owner");
            }
        }

        public void Delete()
        {

        }
    }
}
