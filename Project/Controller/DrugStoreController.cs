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



                ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, "All owners");
                foreach (var owner in owners)
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, $"ID - {owner.ID} Fullname - {owner.Name} {owner.Surname}");
                }
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter owner ID:");
                string id = Console.ReadLine();
                int ownerID;
                bool result = int.TryParse(id, out ownerID);
                if (result)
                {
                    var dbOwner = _ownerRepository.Get(o => o.ID == ownerID);

                    if (dbOwner != null)
                    {
                        DrugStore drugStore = new DrugStore
                        {
                            Name = name,
                            Address = address,
                            ContactNumber = number,
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
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Please, enter owner ID in correct format");
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, $"You must create owner before creating of drugstore");
            }
        }

        public void Update()
        {
            var drugStores = _drugStoreRepository.GetAll();
            if (drugStores.Count > 0)
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Blue, "All drugstore list");
                foreach (var drugstore in drugStores)
                {

                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, $"Drugstore owner info - {drugstore.Owner.ID} {drugstore.Owner.Name}, All info - {drugstore.Name} {drugstore.Address} {drugstore.ContactNumber}");
                }
            ID: ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter owner ID");
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

                        var updatedStore = new DrugStore
                        {
                            Name = Name,
                            Address = Address,
                            ContactNumber = Number,
                        };
                        _drugStoreRepository.Update(updatedStore);
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.Green, $"{updatedStore.Name} {updatedStore.Address} {updatedStore.ContactNumber} is updated to successfully");
                    }
                    else
                    {
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Owner doesn't exist with this ID");
                        goto ID;
                    }
                }
                else
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Enter ID in correct format");
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "There are not any owner");
            }
        }

        public void Delete()
        {
            var drugStores = _drugStoreRepository.GetAll();
            if (drugStores.Count > 0)
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, "All drugstore list");
                foreach (var drugstore in drugStores)
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, $" Drugstore owner info - {drugstore.Owner.ID} {drugstore.Owner.Name}, All info - {drugstore.Name} {drugstore.Address} {drugstore.ContactNumber}");
                }
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter owner ID");
                string id = Console.ReadLine();
                int ownerID;
                var result = int.TryParse(id, out ownerID);
                if (result)
                {
                    var drugstore = _drugStoreRepository.Get(o => o.ID == ownerID);
                    if (drugstore != null)
                    {
                        string allInfo = $"{drugstore.Name} {drugstore.Address}";
                        _drugStoreRepository.Delete(drugstore);
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.Green, $"{allInfo} is deleted  ");
                    }
                    else
                    {
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Drugstore doesn't exist with this ID");
                    }
                }
                else
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Enter ID in correct format");
                }

            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "There are not any drugstore");
            }
        }

        public void GetAll()
        {
            var drugStores = _drugStoreRepository.GetAll();
            if (drugStores.Count > 0)
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, "All drugstore list");
                foreach (var drugstore in drugStores)
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, $"Owner - {drugstore.Owner.Name} All info - {drugstore.Name} {drugstore.Address} {drugstore.ContactNumber}");
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "There is not any drugstores");
            }
        }

        public void GetAllDrugStoresByOwner()
        {
            var owners = _ownerRepository.GetAll();
            if (owners.Count > 0)
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, "All owner list");
                foreach (var owner in owners)
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, $"ID -- {owner.ID}, Fullname - {owner.Name} {owner.Surname}");
                }
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter owner ID");
                string id = Console.ReadLine();
                int ownerID;
                var result = int.TryParse(id, out ownerID);
                if (result)
                {
                    var dbOwner = _ownerRepository.Get(o => o.ID == ownerID);
                    if (dbOwner != null)
                    {
                        var ownerDrugStore = _drugStoreRepository.GetAll(o => o.Owner.ID == ownerID);
                        if (ownerDrugStore.Count != 0)
                        {
                            ConsoleHelper.WriteTextWithColor(ConsoleColor.Green, "All drugstore of the owner");
                            foreach (var ownerdrugstore in ownerDrugStore)
                            {
                                ConsoleHelper.WriteTextWithColor(ConsoleColor.Green, $"ID - {ownerdrugstore.ID} Drugstore Name  - {ownerdrugstore.Name} Drugstore Address {ownerdrugstore.Address}");
                            }
                        }
                        else
                        {
                            ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "There is no owner in this drugstore");
                        }
                    }
                    else
                    {
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Including owner doesn't exist");
                    }
                }
                else
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Red, "Please enter ID in correct format");
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "There is not any owner");
            }
        }

        public void Sale()
        {

        }
    }
}
