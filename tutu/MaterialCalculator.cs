using System;

public class MaterialCalculator
{
    public static int CalculateMaterial(int productTypeID, int materialTypeID, int quantity, double param1, double param2)
    {
        // Получение коэффициента типа продукции и процента брака из базы данных
        double productCoefficient = GetProductCoefficient(productTypeID);
        double defectPercentage = GetDefectPercentage(materialTypeID);

        if (productCoefficient == -1 || defectPercentage == -1)
            return -1;

        double materialPerUnit = param1 * param2 * productCoefficient;
        double totalMaterial = materialPerUnit * quantity;
        double totalMaterialWithDefect = totalMaterial * (1 + defectPercentage);

        return (int)Math.Ceiling(totalMaterialWithDefect);
    }

    private static double GetProductCoefficient(int productTypeID)
    {
        // Запрос к базе данных для получения коэффициента типа продукции
        return 2.35; // Пример значения
    }

    private static double GetDefectPercentage(int materialTypeID)
    {
        // Запрос к базе данных для получения процента брака материала
        return 0.001; // Пример значения
    }
}