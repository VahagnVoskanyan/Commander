﻿using Commander.Models;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(x => x.Id == id);
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null) throw new ArgumentNullException(nameof(cmd));

            _context.Commands.Add(cmd);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            //Nothing
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null) throw new ArgumentNullException(nameof(cmd));

            _context.Commands.Remove(cmd);
        }
    }
}
