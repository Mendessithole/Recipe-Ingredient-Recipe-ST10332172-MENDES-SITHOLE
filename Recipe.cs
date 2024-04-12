using System;

namespace RecipeApp
{
    class Recipe
    {
        private Ingredient[] ingredients;
        private Ingredient[] originalIngredients;
        private Step[] steps;
        private bool isScaled = false;

        // I enter the recipe details.
        public void EnterRecipe()
        {
            Console.WriteLine("\n" + new string('=', Console.WindowWidth - 1));
            Console.WriteLine($"{new string(' ', (Console.WindowWidth - 20) / 2)}\x1b[1mRECIPE INGREDIENTS STEPS\x1b[0m");
            Console.WriteLine(new string('=', Console.WindowWidth - 1));

            Console.WriteLine("\n\x1b[1mIngredients\x1b[0m");
            Console.WriteLine(new string('-', Console.WindowWidth - 1));

            Console.WriteLine("Enter the number of ingredients:");
            int numIngredients;
            while (!int.TryParse(Console.ReadLine(), out numIngredients) || numIngredients <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
            ingredients = new Ingredient[numIngredients];
            originalIngredients = new Ingredient[numIngredients];

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter details for ingredient #{i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                double quantity;
                Console.Write("Quantity: ");
                while (!double.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a positive number.");
                }
                Console.Write("Unit: ");
                string unit = Console.ReadLine();
                ingredients[i] = new Ingredient { Name = name, Quantity = quantity, Unit = unit };
                originalIngredients[i] = new Ingredient { Name = name, Quantity = quantity, Unit = unit };
            }

            Console.WriteLine("\n\x1b[1mSteps\x1b[0m");
            Console.WriteLine(new string('-', Console.WindowWidth - 1));

            Console.WriteLine("Enter the number of steps:");
            int numSteps;
            while (!int.TryParse(Console.ReadLine(), out numSteps) || numSteps <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
            steps = new Step[numSteps];

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step #{i + 1}:");
                string description = Console.ReadLine();
                steps[i] = new Step { Description = description };
            }

            // I automatically display the recipe after entering.
            DisplayRecipe();

            // Ask if the user wants to scale the recipe.
            string scaleChoice;
            do
            {
                Console.WriteLine("\nDo you want to scale the recipe? (yes/no)");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
                scaleChoice = Console.ReadLine().ToLower();
                Console.ResetColor();
            } while (scaleChoice != "yes" && scaleChoice != "no");

            if (scaleChoice == "yes")
            {
                ScaleRecipe();
            }

            // Ask if the user wants to reset the scale.
            string resetChoice;
            do
            {
                Console.WriteLine("\nDo you want to reset the scale? (yes/no)");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
                resetChoice = Console.ReadLine().ToLower();
                Console.ResetColor();
            } while (resetChoice != "yes" && resetChoice != "no");

            if (resetChoice == "yes")
            {
                ResetScale();
            }

            // Ask if the user wants to clear the recipe.
            string clearChoice;
            do
            {
                Console.WriteLine("\nDo you want to clear the recipe? (yes/no)");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
                clearChoice = Console.ReadLine().ToLower();
                Console.ResetColor();
            } while (clearChoice != "yes" && clearChoice != "no");

            if (clearChoice == "yes")
            {
                ClearRecipe();
            }

            // Congratulate the user and ask if they want to create a new recipe.
            Console.WriteLine("\nCongratulations on creating your recipe!");
            Console.WriteLine("Click any button to create a new recipe.");

            // Wait for user input before starting a new recipe.
            Console.ReadKey(true);

            EnterRecipe();
        }

        // I display the entered recipe.
        public void DisplayRecipe()
        {
            Console.WriteLine($"\n{new string('=', Console.WindowWidth - 1)}");
            Console.WriteLine("\n\x1b[1mRECIPE\x1b[0m:");
            Console.WriteLine("\n\x1b[1mIngredients\x1b[0m:");
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
            }

            Console.WriteLine("\n\x1b[1mSteps\x1b[0m:");
            for (int i = 0; i < steps.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i].Description}");
            }
        }

        // I scale the recipe by the given factor.
        public void ScaleRecipe()
        {
            if (!isScaled)
            {
                Console.WriteLine("Enter scale factor (0.5, 2, or 3):");
                double factor;
                while (!double.TryParse(Console.ReadLine(), out factor) || (factor != 0.5 && factor != 2 && factor != 3))
                {
                    Console.WriteLine("Invalid input. Please enter 0.5, 2, or 3.");
                }
                foreach (var ingredient in ingredients)
                {
                    ingredient.Quantity *= factor;
                }
                isScaled = true;
                Console.WriteLine("\nRecipe has been scaled.");
                DisplayRecipe();
            }
            else
            {
                Console.WriteLine("Recipe has already been scaled. Please reset scale to scale again.");
            }
        }

        // I reset the scale of the recipe to its original quantities.
        public void ResetScale()
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                ingredients[i].Quantity = originalIngredients[i].Quantity;
            }
            isScaled = false;
            Console.WriteLine("\nScale reset.");
            DisplayRecipe();
        }

        // I clear the recipe details.
        public void ClearRecipe()
        {
            ingredients = null;
            steps = null;
            Console.WriteLine("\nRecipe cleared.");
        }
    }
}
