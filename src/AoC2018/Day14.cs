using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day14
    {
        public static string Part1(int recipeCount)
        {
            var board = new Board();

            while (board.Recipes.Count < recipeCount + 10)
            {
                board.CreateNewRecipe();
            }

            return board.GetScore(recipeCount);
        }

        public static int Part2(string input)
        {
            var board = new Board();

            var score = input.Select(c => int.Parse(c.ToString())).ToArray();
            foreach (var j in Enumerable.Range(0, 100_000_000))
            {
                board.CreateNewRecipe();

                var i = board.FindMatchingScoreIndex(score);
                if (i > -1)
                {
                    return i;
                }
            }
            throw new InvalidOperationException();
        }

        public class Board
        {
            public List<int> Recipes { get; set; } = new List<int> { 3, 7 };

            public int Elf1Index { get; set; } = 0;

            public int Elf2Index { get; set; } = 1;

            public string GetScore(int recipeCount) =>
                string.Join("", Recipes.Skip(recipeCount).Select(i => i.ToString()));

            public void CreateNewRecipe()
            {
                var newRecipes = CalculateNewRecipes();
                Recipes.AddRange(newRecipes);
                Elf1Index = (Elf1Index + Recipes[Elf1Index] + 1) % Recipes.Count;
                Elf2Index = (Elf2Index + Recipes[Elf2Index] + 1) % Recipes.Count;
            }

            public int[] CalculateNewRecipes() =>
                (Recipes[Elf1Index] + Recipes[Elf2Index]).ToString()
                    .ToCharArray()
                    .Select(c => int.Parse(c.ToString()))
                    .ToArray();

            public override string ToString() => string.Join(" ", Recipes.Select(i => i.ToString()));

            public int FindMatchingScoreIndex(int[] score)
            {
                if (Recipes.Count - 1 < score.Length) return -1;
                foreach (var j in new[] { -1, 0 })
                {
                    bool matches = true;
                    for (var i = 0; i < score.Length; i++)
                    {
                        var rs = Recipes[Recipes.Count - score.Length + i + j];
                        if (rs != score[i])
                        {
                            matches = false;
                            break;
                        }
                    }
                    if (matches) return Recipes.Count - score.Length + j;
                }
                return -1;
            }
        }
    }
}
