using System;
using System.Collections.Generic;

public class Student
{
    public string Name { get; set; }
    public string Group { get; set; }
    public double AverageScore { get; set; } 

    public Student(string name, string group, double avgScore)
    {
        Name = name;
        Group = group;
        AverageScore = avgScore;
    }

    public override string ToString()
    {
        return $"Student: {Name}, Group: {Group}, AvgScore: {AverageScore:F2}";
    }
}

public class Node
{
    public Student Data { get; set; }
    public Node Prev { get; set; }
    public Node Next { get; set; }

    public Node(Student data)
    {
        Data = data;
        Prev = null;
        Next = null;
    }
}

public class DoublyLinkedList
{
    private Node head;
    private Node tail;
    public void AddLast(Student student)
    {
        Node newNode = new Node(student);
        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }

    public bool RemoveLast()
    {
        if (tail == null) return false;

        if (head == tail)
        {
            head = tail = null;
        }
        else
        {
            tail = tail.Prev;
            tail.Next = null;
        }
        return true;
    }

    public Student this[int index]
    {
        get
        {
            if (index < 0) throw new IndexOutOfRangeException();
            Node current = head;
            int i = 0;
            while (current != null)
            {
                if (i == index) return current.Data;
                current = current.Next;
                i++;
            }
            throw new IndexOutOfRangeException();
        }
        set
        {
            if (index < 0) throw new IndexOutOfRangeException();
            Node current = head;
            int i = 0;
            while (current != null)
            {
                if (i == index)
                {
                    current.Data = value;
                    return;
                }
                current = current.Next;
                i++;
            }
            throw new IndexOutOfRangeException();
        }
    }

    public int Length
    {
        get
        {
            int count = 0;
            Node current = head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
    }

    public Node GetFirst() => head;

    public Node GetNext(Node current)
    {
        return current?.Next;
    }

    public double GetMedianAverageScore()
    {
        int n = Length;
        if (n == 0) throw new InvalidOperationException("Список порожній");

        List<double> scores = new List<double>();
        Node current = head;
        while (current != null)
        {
            scores.Add(current.Data.AverageScore);
            current = current.Next;
        }

        scores.Sort();

        if (n % 2 == 1)
            return scores[n / 2];
        else
            return (scores[n / 2 - 1] + scores[n / 2]) / 2.0;
    }

    public void PrintList()
    {
        Node current = head;
        while (current != null)
        {
            Console.WriteLine(current.Data);
            current = current.Next;
        }
    }
}

class Program
{
    static void Main()
    {
        DoublyLinkedList list = new DoublyLinkedList();

        list.AddLast(new Student("Іваненко І.", "КН-21", 85.5));
        list.AddLast(new Student("Петренко П.", "КН-21", 92.0));
        list.AddLast(new Student("Сидоренко С.", "КН-22", 78.3));
        list.AddLast(new Student("Коваленко К.", "КН-21", 88.7));

        Console.WriteLine("Список студентів:");
        list.PrintList();

        Console.WriteLine($"\nДовжина списку: {list.Length}");
        Console.WriteLine($"Медіанний середній бал: {list.GetMedianAverageScore():F2}");

        // Використання індексатора
        Console.WriteLine("\nЕлемент за індексом 1: " + list[1]);
        list[1] = new Student("Петренко Оновлений", "КН-21", 95.0);
        Console.WriteLine("Після зміни:");
        list.PrintList();

        list.RemoveLast();
        Console.WriteLine("\nПісля видалення останнього:");
        list.PrintList();
    }
}