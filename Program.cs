using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("Введите первую строку (или 'exit' для выхода): ");
            string str1 = Console.ReadLine();

            if (str1.ToLower() == "exit")
                break;

            Console.Write("Введите вторую строку: ");
            string str2 = Console.ReadLine();

            int distance = DamerauLevenshteinDistance(str1, str2);

            Console.WriteLine($"Расстояние Дамерау-Левенштейна: {distance}");
            Console.WriteLine();
        }
    }

    static int DamerauLevenshteinDistance(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1))
            return s2?.Length ?? 0;

        if (string.IsNullOrEmpty(s2))
            return s1.Length;

        int len1 = s1.Length;
        int len2 = s2.Length;

        int[,] matrix = new int[len1 + 1, len2 + 1];

        // Инициализация
        for (int i = 0; i <= len1; i++)
            matrix[i, 0] = i;

        for (int j = 0; j <= len2; j++)
            matrix[0, j] = j;

        // Заполнение матрицы
        for (int i = 1; i <= len1; i++)
        {
            for (int j = 1; j <= len2; j++)
            {
                int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;

                int deletion = matrix[i - 1, j] + 1;
                int insertion = matrix[i, j - 1] + 1;
                int substitution = matrix[i - 1, j - 1] + cost;

                matrix[i, j] = Math.Min(Math.Min(deletion, insertion), substitution);

                // Проверка транспозиции (поправка Дамерау)
                if (i > 1 && j > 1 &&
                    s1[i - 1] == s2[j - 2] &&
                    s1[i - 2] == s2[j - 1])
                {
                    matrix[i, j] = Math.Min(matrix[i, j],
                        matrix[i - 2, j - 2] + 1);
                }
            }
        }

        return matrix[len1, len2];
    }
}