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
    public class DrugGistController
    {
        private DrugGistRepository _drugGistRepository;
        private DrugStoreRepository _drugStoreRepository;
        public DrugGistController()
        {
            _drugGistRepository = new DrugGistRepository();
            _drugStoreRepository = new DrugStoreRepository();
        }

        public void Create()
        {
            var drugstores = _drugStoreRepository.GetAll();
            if (drugstores.Count != 0)
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "Enter druggist name");
                string name = Console.ReadLine();

                ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "Enter druggist surname");
                string surname = Console.ReadLine();

                ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "Enter druggist age");
                string age = Console.ReadLine();
                string drugGistAge;
                //bool result = byte.TryParse(age, out drugGistAge);


                ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "Enter druggist experience");
                string experience = Console.ReadLine();
                byte drugGistExperience;
                bool result1 = byte.TryParse(experience, out drugGistExperience);

                ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, "All drugstores");
                foreach (var drugstore in drugstores)
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, $" ID - {drugstore.ID} Name - {drugstore.Name}");
                }
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Yellow, "Enter drugstore ID");
                string id = Console.ReadLine();
                int drugstoreID;
                bool result2 = int.TryParse(id, out drugstoreID);
                if (result2)
                {
                    var dbDrugStore = _drugStoreRepository.Get(d => d.ID == drugstoreID);
                    if (dbDrugStore != null)
                    {
                        DrugGist drugGist = new DrugGist
                        {
                            Name = name,
                            Surname = surname,
                            //Age = drugGistAge,
                            Experience = drugGistExperience,
                        };
                        _drugGistRepository.Create(drugGist);
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.Green, $"Name - {drugGist.Name} Surname - {drugGist.Surname} Age - {drugGist.Age} Experience - {drugGist.Experience}");
                    }
                    else
                    {
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "Including drugstore doesn't exist");
                    }

                }
                else
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "Please, enter drugstore ID in correct format");
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "You must create drugstore before creating of druggist");
            }
        }

        public void Update()
        {

        }

        public void Delete()
        {
            var druggists = _drugGistRepository.GetAll();
            if (druggists.Count > 0)
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, "All druggist list");
                foreach (var druggist in druggists)
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, $"ID - {druggist.ID} Name - {druggist.Name} Surname - {druggist.Surname} Age - {druggist.Age}");
                }
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter druggist ID");
                string id = Console.ReadLine();
                int drugGistID;
                bool result = int.TryParse(id, out drugGistID);
                if (result)
                {
                    var druggist = _drugGistRepository.Get(d => d.ID == drugGistID);
                    if (druggist != null)
                    {
                        string AllInfo = $"{druggist.Name} {druggist.Surname}";
                        _drugGistRepository.Delete(druggist);
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.Green, $"{AllInfo} is deleted");
                    }
                    else
                    {
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "Druggist doesn't exist with this ID");
                    }
                }
                else
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "Please, enter druggist ID in correct format");
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "There are not any druggist");
            }
        }

        public void GetAll()
        {
            var druggists = _drugGistRepository.GetAll();
            if (druggists.Count > 0)
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, "All druggist list");
                foreach (var druggist in druggists)
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, $"ID -- {druggist.ID} Name - {druggist.Surname} Surname - {druggist.Surname} Age - {druggist.Age} Experience - {druggist.Experience}");
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "There is not any druggists");
            }
        }

        public void GetAllDruggistByDrugstore()
        {
            var drugstores = _drugStoreRepository.GetAll();
            if (drugstores.Count > 0)
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, "All drugstore list");
                foreach (var drugstore in drugstores)
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.Cyan, $"ID -- {drugstore.ID} Name -- {drugstore.Name} Address -- {drugstore.Address} ");
                }
                ConsoleHelper.WriteTextWithColor(ConsoleColor.Magenta, "Enter drugstore ID");
                string id = Console.ReadLine();
                int drugStoreID;
                var result = int.TryParse(id, out drugStoreID);
                if (result)
                {
                    var dbDrugStore = _drugStoreRepository.Get(d => d.ID == drugStoreID);
                    if (dbDrugStore != null)
                    {
                        var drugGistStore = _drugGistRepository.GetAll(d => d.ID == drugStoreID);
                        if (drugGistStore.Count != 0)
                        {
                            ConsoleHelper.WriteTextWithColor(ConsoleColor.Green, "All druggist of the drugstore");
                            foreach (var druggistStore in drugGistStore)
                            {
                                ConsoleHelper.WriteTextWithColor(ConsoleColor.Green, $"ID -- {druggistStore.ID} Druggist all info - {druggistStore.Name} {druggistStore.Surname} {druggistStore.Age} {druggistStore.Experience}");
                            }
                        }
                        else
                        {
                            ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "There is no drugstore in this druggist");
                        }
                    }
                    else
                    {
                        ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "Druggist doesn't exist with this ID");
                    }
                }
                else
                {
                    ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed, "Please, enter druggist ID in correct format");
                }
            }
            else
            {
                ConsoleHelper.WriteTextWithColor(ConsoleColor.DarkRed,"There is not any druggist");
            }
        }

    }
}
