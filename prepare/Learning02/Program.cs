using System;

class Program
{
    static void Main(string[] args)
    {
        Job jackInTheBox = new Job("Team Member",2021,2021, "Feast Foods LLC");
        Job pizzaPieCafe = new Job("Team Member",2022,2022,"Lindsay LLC");
        Job blueBirdExpress = new Job("Team Member",2023,2024, "Bluebird Express Carwash LLC");
        Job av = new Job("Video engineer",2025,2025, "BYUI AV productions");


        Resume resume = new Resume("Jordan");
        resume._jobs.Add(jackInTheBox);
        resume._jobs.Add(pizzaPieCafe);
        resume._jobs.Add(blueBirdExpress);
        resume._jobs.Add(av);

        resume.Display();
    }
}