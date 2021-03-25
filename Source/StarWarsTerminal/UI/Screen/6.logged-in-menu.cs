﻿using System;
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
         public static Option AccountScreen()
        {
            ConsoleWriter.ClearScreen();
            string[] lines = File.ReadAllLines(@"UI/TextFrames/6.logged-in-menu.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var parameterCoords = drawables.FindAll(x => x.Chars == "¤").ToList().Select(x => (x.CoordinateX, x.CoordinateY)).ToList();

            var nameCoord = parameterCoords[0];
            var shipCoord = parameterCoords[1];

            var selectionList = new SelectionList<Option>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new Option[] 
            {
                Option.Park, 
                Option.CheckReceipts,
                Option.ReRegisterShip,
                Option.GoToHomeplanet,
                Option.Exit 
            });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            Console.ForegroundColor = ConsoleColor.Green;
            LineTools.SetCursor(nameCoord);
            Console.Write(_account.User.Name);
            LineTools.SetCursor(shipCoord);
            Console.Write(_account.SpaceShip.Model);

            return selectionList.GetSelection();
        }
    }
}
