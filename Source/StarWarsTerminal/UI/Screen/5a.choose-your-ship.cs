﻿using StarWarsApi.Database;
using StarWarsApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screen
{
    public static partial class Screen
    {
        public static Option RegisterShip()
        {
            ConsoleWriter.ClearScreen();
            string[] lines = File.ReadAllLines(@"UI/TextFrames/5a.choose-your-ship.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            int nextLine = drawables.Max(x => x.CoordinateY);
            var localShips = GetLocalShips();
            string[] shipLines = localShips.Select(x => "$ " + x.Model).ToArray();
            drawables.AddRange(TextEditor.Add.DrawablesAt(shipLines, nextLine + 3));
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);

            var selectionList = new SelectionList<SpaceShip>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(localShips);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            _ship = selectionList.GetSelection();
            DatabaseManagement.AccountManagement.Register(_ship, _namepass);

            return Option.Login;
        }
    }
}
