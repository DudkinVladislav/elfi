using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApp2
{

    public partial class Form1 : Form
    {
        Random random = new Random();
        int moves = 0;
        int diametR = 10;
        public partial class creature
        {
            public int coord_x;
            public int coord_y;
            public int saturation; //насыщение
            public int age;
            public SolidBrush color;
            public creature(int X, int Y)
            {
                this.coord_x = X;
                this.coord_y = Y;
                this.saturation = 20;
                this.age = 0;
                Random random = new Random();
                int sex = random.Next(2);
                if (sex == 0)
                { this.color = new SolidBrush(Color.Blue); }
                else { this.color = new SolidBrush(Color.Red); }
            }
        }
        public partial class food
        {
            public int coord_x;
            public int coord_y;
            public SolidBrush color;
            public food(int X, int Y)
            {
                this.coord_x = X;
                this.coord_y = Y;
                this.color = new SolidBrush(Color.Green);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            moves = 0;
            List<creature> male = new List<creature>();
            List<creature> female = new List<creature>();
            List<food> foods = new List<food>();
            creature first_m = new creature(1024 / 2, 768 / 2);
            first_m.color = new SolidBrush(Color.Blue);
            creature first_f = new creature(1024 / 2 + 5, 768 / 2 + 5);
            first_f.color = new SolidBrush(Color.Red);
            male.Add(first_m);
            female.Add(first_f);
            int kol = random.Next(91) + 10;
           
            for (int i = 0; i < kol; i++)
            {
                int food_position_x = random.Next(1024 - diametR);
                int food_position_y = random.Next(768 - diametR);

                food f = new food(food_position_x, food_position_y);
                foods.Add(f);
            }
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.Black);
            for (int i = 0; i < male.Count; i++)
                g.FillEllipse(male[i].color, male[i].coord_x, male[i].coord_y, diametR, diametR);
            for (int i = 0; i < female.Count; i++)
                g.FillEllipse(female[i].color, female[i].coord_x, female[i].coord_y, diametR, diametR);
            for (int i = 0; i < foods.Count; i++)
                g.FillEllipse(foods[i].color, foods[i].coord_x, foods[i].coord_y, diametR, diametR);

            Thread BirthEat = new Thread(Birth);
            BirthEat.Start();
            void Birth()
            {
               Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                int maxalives = male.Count+female.Count;
                while (foods.Count > 0 && male.Count + female.Count > 0)
            {
                    int temp = male.Count + female.Count;
                    if(temp > maxalives) { 
                    maxalives=temp ;
                    }
                    for (int i = 0; i < male.Count; i++)
                        for (int j = 0; j <female.Count; j++)
                        {
                            
                            if (Math.Abs(male[i].coord_x - female[j].coord_x) < 9 && Math.Abs(male[i].coord_y - female[j].coord_y) < 9)
                            {
                                int number = 0;
                                if (male[i].saturation <= 60 || female[j].saturation <= 60)
                                {
                                    number = 1;
                                }
                                if (male[i].saturation <= 40 && female[j].saturation <= 40)
                                {
                                    number = 2;
                                }
                                if (male[i].saturation <= 20 || female[j].saturation <= 20)
                                {
                                    number = 3;
                                }
                                if (male[i].saturation <= 10 && female[j].saturation <= 10)
                                {
                                    number = 4;
                                }
                                Console.WriteLine(number);
                                int exist = 1;
                                if (female.Count + male.Count > foods.Count * 4)
                                {
                                    for (int k = 0; k < number; k++)
                                    {
                                        int koef = random.Next(9) + 2;
                                        int y = 0;
                                        int x = 0;
                                        int pachka = random.Next(5);
                                        if (pachka == 0)
                                        {
                                            y = random.Next(diametR / 2 * koef + diametR / 2 * koef + Math.Abs(male[i].coord_y - female[j].coord_y)) - diametR / 2 * koef + Math.Min(male[i].coord_y, female[j].coord_y);
                                            x = Math.Min(male[i].coord_x, female[j].coord_x) - diametR / 2 * koef + random.Next(diametR / 2 * koef - 10);

                                        }
                                        if (pachka == 1)
                                        {
                                            x = random.Next(diametR / 2 * koef + diametR / 2 * koef + Math.Abs(male[i].coord_x - female[j].coord_x)) - diametR / 2 * koef + Math.Min(male[i].coord_x, female[j].coord_x);
                                            y = Math.Max(male[i].coord_y, female[j].coord_y) + 10 + random.Next(diametR / 2 * koef - 10);

                                        }
                                        if (pachka == 2)
                                        {
                                            x = random.Next(diametR / 2 * koef + diametR / 2 * koef + Math.Abs(male[i].coord_x - female[j].coord_x)) - 9 + Math.Min(male[i].coord_x, female[j].coord_x);
                                            y = Math.Min(male[i].coord_y, female[j].coord_y) - diametR / 2 * koef + random.Next(diametR / 2 * koef - 10);

                                        }
                                        if (pachka == 3)
                                        {
                                            y = random.Next(diametR / 2 * koef + diametR / 2 * koef + Math.Abs(male[i].coord_y - female[j].coord_y)) - 9 + Math.Min(male[i].coord_y, female[j].coord_y);
                                            x = Math.Max(male[i].coord_x, female[j].coord_x) + random.Next(diametR / 2 * koef - 10) + 10;

                                        }
                                        if (pachka == 4)
                                        {
                                            int p = random.Next(2);
                                            if (p == 0)
                                            {
                                                x = random.Next(Math.Abs(male[i].coord_x - female[j].coord_x)) + diametR + Math.Min(male[i].coord_x, female[j].coord_x);
                                                y = random.Next(Math.Abs(male[i].coord_y - female[j].coord_y)) - diametR + Math.Min(male[i].coord_y, female[j].coord_y);
                                            }
                                            if (p == 1)
                                            {
                                                x = random.Next(Math.Abs(male[i].coord_x - female[j].coord_x)) - diametR + Math.Max(male[i].coord_x, female[j].coord_x);
                                                y = random.Next(Math.Abs(male[i].coord_y - female[j].coord_y)) + diametR + Math.Min(male[i].coord_y, female[j].coord_y);
                                            }

                                        }
                                        
                                        if ((x < 0) || (y < 0) || (x + diametR > 1024) || (y + diametR > 768))
                                        {
                                            exist = 0;
                                        }
                                        if (exist == 1)
                                        {
                                            creature cr = new creature(x, y);
                                            SolidBrush r = new SolidBrush(Color.Red);
                                            SolidBrush b = new SolidBrush(Color.Blue);
                                            if (cr.color == r) { female.Add(cr); }
                                            else { male.Add(cr); }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < number; k++)
                                    {
                                        int y = 0;
                                        int x = 0;
                                        int pachka = random.Next(5);
                                        if (pachka == 0)
                                        {
                                            y = random.Next(10 + 10 + Math.Abs(male[i].coord_y - female[j].coord_y)) - 10 + Math.Min(male[i].coord_y, female[j].coord_y);
                                            x = Math.Min(male[i].coord_x, female[j].coord_x) - 10;

                                        }
                                        if (pachka == 1)
                                        {
                                            x = random.Next(10 + 10 + Math.Abs(male[i].coord_x - female[j].coord_x)) - 10 + Math.Min(male[i].coord_x, female[j].coord_x);
                                            y = Math.Max(male[i].coord_y, female[j].coord_y) + 10;

                                        }
                                        if (pachka == 2)
                                        {
                                            x = random.Next(10 + 10 + Math.Abs(male[i].coord_x - female[j].coord_x)) - 9 + Math.Min(male[i].coord_x, female[j].coord_x);
                                            y = Math.Min(male[i].coord_y, female[j].coord_y) - 10;

                                        }
                                        if (pachka == 3)
                                        {
                                            y = random.Next(10 + 10 + Math.Abs(male[i].coord_y - female[j].coord_y)) - 9 + Math.Min(male[i].coord_y, female[j].coord_y);
                                            x = Math.Max(male[i].coord_x, female[j].coord_x) + 10;

                                        }
                                        if (pachka == 4)
                                        {
                                            int p = random.Next(2);
                                            if (p == 0)
                                            {
                                                x = random.Next(Math.Abs(male[i].coord_x - female[j].coord_x)) + 10 + Math.Min(male[i].coord_x, female[j].coord_x);
                                                y = random.Next(Math.Abs(male[i].coord_y - female[j].coord_y)) - 10 + Math.Min(male[i].coord_y, female[j].coord_y);
                                            }
                                            if (p == 1)
                                            {
                                                x = random.Next(Math.Abs(male[i].coord_x - female[j].coord_x)) - 10 + Math.Max(male[i].coord_x, female[j].coord_x);
                                                y = random.Next(Math.Abs(male[i].coord_y - female[j].coord_y)) + 10 + Math.Min(male[i].coord_y, female[j].coord_y);
                                            }

                                        }
                                        
                                        if ((x < 0) || (y < 0) || (x + 10 > 1024) || (y + 10 > 768))
                                        {
                                            exist = 0;
                                        }
                                        if (exist == 1)
                                        {
                                            creature cr = new creature(x, y);
                                            SolidBrush r = new SolidBrush(Color.Red);
                                            SolidBrush b = new SolidBrush(Color.Blue);
                                            if (cr.color == r) { female.Add(cr); }
                                            else { male.Add(cr); }
                                        }
                                    }
                                }



                            }
                        }

                    
                    for (int i = 0; i < male.Count; i++)
                            for (int k = 0; k < foods.Count; k++)
                            {
                                lock (male[i])
                                {
                                    lock (foods[k])
                                    {
                                        if (Math.Abs(male[i].coord_x - foods[k].coord_x) < 9 && Math.Abs(male[i].coord_y - foods[k].coord_y) < 9)
                                        {
                                            foods.RemoveAt(k);
                                              male[i].saturation += 20;
                                        Console.WriteLine("male eat");
                                        }
                                    }
                                }
                            }
                    for (int j = female.Count - 1; j >= 0; j--)
                        for (int k = 0; k < foods.Count; k++)
                        {
                            lock (female[j])
                            {
                                lock (foods[k])
                                {
                                    if (Math.Abs(female[j].coord_x - foods[k].coord_x) < 9 && Math.Abs(female[j].coord_y - foods[k].coord_y) < 9)
                                    {
                                        foods.RemoveAt(k);
                                        female[j].saturation += 20;
                                        Console.WriteLine("female eat");
                                    }
                                }
                            }
                        }
                    if (moves % 100 == 0)
                    {
                        kol = random.Next(91) + 10;
                        for (int i = 0; i < kol; i++)
                        {
                            int food_position_x = random.Next(1024 - diametR * 2) + diametR;
                            int food_position_y = random.Next(768 - diametR * 2) + diametR;

                            food f = new food(food_position_x, food_position_y);
                            foods.Add(f);
                        }
                    }
                    Thread ThreadMale = new Thread(MaleMoves);
                    Thread ThreadFemale = new Thread(FemaleMoves);
                    
                    // запускаем поток Thread
                    ThreadMale.Start();
                    ThreadFemale.Start();
                    Thread.Sleep(200);
                    ThreadMale.Join();
                    ThreadFemale.Join();
                    void MaleMoves()
                    {
                        for (int i = 0; i < male.Count; i++)
                        {
                            int way = random.Next(4);
                            lock (male[i])
                            {
                                if (way == 1)
                                    male[i].coord_x += 10;
                                if (way == 0)
                                    male[i].coord_y -= 10;
                                if (way == 2)
                                    male[i].coord_y += 10;
                                if (way == 3)
                                    male[i].coord_x -= 10;
                               
                            }
                        }
                        for (int i = 0; i < male.Count; i++)
                        {
                            if ((male[i].coord_x <= 0) || (male[i].coord_y <= 0) || (male[i].coord_x >= 1024 - 9) || (male[i].coord_y >= 768 - 9))
                                male.RemoveAt(i);
                            male[i].age += 1;
                            int ok = 0;
                            male[i].saturation -= 1;
                            if (male[i].age == 500)
                            {
                                ok = 1;
                                male.RemoveAt(i);
                            }
                            if ((male[i].saturation == 0)&&(ok==0))
                            {
                                Console.WriteLine("food death male");
                                male.RemoveAt(i);
                            }
                        }
                        moves += 1;
                        
                    }
                    // запускаем поток myThread

                    void FemaleMoves()
                    {

                        for (int i = female.Count - 1; i >= 0; i--)
                        {
                            lock (female[i])
                            {
                                int way = random.Next(4);
                                if (way == 1)
                                    female[i].coord_x += 10;
                                if (way == 0)
                                    female[i].coord_y -= 10;
                                if (way == 2)
                                    female[i].coord_y += 10;
                                if (way == 3)
                                    female[i].coord_x -= 10;
                            }
                        }
                        for (int i = 0; i < female.Count; i++)
                        {
                            if ((female[i].coord_x <= 0) || (female[i].coord_y <= 0) || (female[i].coord_x >= 1024 - 9) || (female[i].coord_y >= 768 - 9))
                                female.RemoveAt(i);
                            female[i].age += 1;
                            female[i].saturation -= 1;
                            if (female[i].age == 500)
                                female.RemoveAt(i);
                            if (female[i].saturation == 0)
                            {
                                Console.WriteLine("food death female");
                                female.RemoveAt(i);
                            }
                        }
                        
                    }
                    g.Clear(Color.Black);
                    for (int i = 0; i < male.Count; i++)
                        g.FillEllipse(male[i].color, male[i].coord_x, male[i].coord_y, diametR, diametR);
                    for (int i = 0; i < female.Count; i++)
                        g.FillEllipse(female[i].color, female[i].coord_x, female[i].coord_y, diametR, diametR);
                    for (int i = 0; i < foods.Count; i++)
                        g.FillEllipse(foods[i].color, foods[i].coord_x, foods[i].coord_y, diametR, diametR);
                    Thread.Sleep(100);
                }
                stopwatch.Stop();
                MessageBox.Show("Количество шагов жизни: "+moves.ToString() + "\nМаксимальное количество особей в один момент времени: " + maxalives+" \nВремя жизни: "+ stopwatch.ElapsedMilliseconds+ " миллисекунд");
                Console.WriteLine(moves);
                Console.WriteLine(male.Count + " " + female.Count);
            
            }
            
        }
    }
}
