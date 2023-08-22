using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public List<Command> Commands { get; set; }
        public MockCommanderRepo()
        {
            Commands = new List<Command>
            {
                new Command { Id = 0, HowTo = "How to create Migrations", Line = "dotnet ef migrations add <Name of Migration>", Platform = ".NET Core" },
                new Command { Id = 1, HowTo = "How to run Migrations", Line = "dotnet ef database update", Platform = ".NET Core" },
                new Command { Id = 2, HowTo = "How to roll back a Migration", Line = "dotnet ef migrations remove", Platform = ".NET Core" }
            };
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null) throw new ArgumentNullException(nameof(cmd));

            Commands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null) throw new ArgumentNullException(nameof(cmd));

            Commands.Remove(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return Commands;
        }

        public Command GetCommandById(int id)
        {
            return Commands.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            //Nothing

            return true;
        }

        public void UpdateCommand(Command cmd)
        {
            //Nothing
        }
    }
}
