using System;
//Class for calculating mathematical tasks of the game
public class MathGenerator 
{
    const int hardUp = 7;
    const int buttonQuantity = 3;
    char[] sign = new char[2] { '+', '-' }; //All math operation in the game
    Random gen = new();

    //Object constructor for every math request
    public MathGenerator(int level, out string taskText, out int correctButton, out int correct, out int error1, out int error2)
    {
        taskText = GenerateTask(level, out correct, out error1, out error2);
        correctButton = gen.Next(buttonQuantity);
    }

    //Method for generating random math tasks (operands and operations)
    public string GenerateTask(int level, out int correct, out int error1, out int error2)
    {
        int[] operands = new int[(level / hardUp) + 2];
        char[] operations = new char[level / hardUp + 1];
        for (int i = 0; i < operands.Length; i++)
        {
            operands[i] = gen.Next(-10, 11);
            if (i < operations.Length) operations[i] = sign[gen.Next(sign.Length)];
        }

        return GetCorrectAnswer(operations, operands, out correct, out error1, out error2); 
    }

    //Method for generating answers to generated tasks
    public string GetCorrectAnswer(char[] operations, int[] operand, out int correct, out int error1, out int error2)
    {
        int answer = operand[0];
        string taskText = operand[0].ToString();
        for(int i = 0; i < operations.Length; i++) //Apply all operations from array
        {  
            switch (operations[i])
            {
                case '+':
                    answer +=  operand[i + 1];
                    break;
                case '-':
                    answer -= operand[i + 1];
                    break;
                default:
                    
                    break;

            }
            taskText += (" " + operations[i] + " " + (operand[i + 1] < 0 ? "(" + operand[i + 1] + ")" : operand[i + 1])); //Create task string
        }
        
        correct = answer;
        while (true) //Generating correct and incorrect answers with matching check
        {
            error1 = gen.Next(correct - 5, correct + 5);
            error2 = gen.Next(correct - 5, correct + 5);
            if(error1 != correct && error2 != correct && error1 != error2)
            {
                break;
            }
        }
        return (taskText + " =");
    }

}
