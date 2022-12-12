using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDP_odev_2
{ 
    public partial class Form1 : Form
    {
        public Form1()
        {


            InitializeComponent();

            this.Text = "BILL CONVERTER";           
            Label lbl_number = new Label(); // The label is created as a title.
            lbl_number.Text = "NUMBER";
            lbl_number.Top = 55;
            lbl_number.Left = 100;
             //The textbox is positioned 
            lbl_number.AutoSize = true; //It will take size according to the character length entered in the label.
            lbl_number.BackColor = Color.Orange; 
            this.Controls.Add(lbl_number); // The label is added to the form

            TextBox txt_number = new TextBox(); //Textbox is created to enter a number
            txt_number.Top = 50;
            txt_number.Left = 200;
            txt_number.Width = 150;
            this.Controls.Add(txt_number); 
        
            Label lbl_text_of_number = new Label(); //The Label is created as a title.
            lbl_text_of_number.Text = "TEXT";
            lbl_text_of_number.Top = 90;
            lbl_text_of_number.Left = 100;
            lbl_text_of_number.AutoSize = true; 
            lbl_text_of_number.BackColor = Color.Orange;    
            this.Controls.Add(lbl_text_of_number);

            Label lbl_text = new Label(); //The label is created to show the text version of the entered number
            lbl_text.Top = 90;
            lbl_text.Left = 200;
            lbl_text.AutoSize = true; 
            lbl_text.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            lbl_text.BackColor = Color.Brown;
            lbl_text.ForeColor = Color.White;
            //The background and foreground colors of the label are changed.
            this.Controls.Add(lbl_text);

            Button btn_calculate= new Button(); //The button is created to calculate.
            btn_calculate.Text = "HESAPLA";
            btn_calculate.Top = 150;
            btn_calculate.Left = 100;

            this.Controls.Add(btn_calculate); 
            btn_calculate.Click += new EventHandler(Calculate); //When the button is pressed, the operations of the 'Calculate' function will be worked.

            void Calculate (object sender, EventArgs e)
            {
                bool control = true, decimal_part = true, whole_part = true;
                int whole = 0, decimal = 0, counter = 0;
                string decimal_place, whole_place;

                string Number = txt_number.Text; // The variable 'Number' takes the number entered in the text as a string.


                if (Number == "" || Number==" " || Number=="  ") //This error will be thrown if nothing is entered in the textBox.
                {
                    control = false;
                    MessageBox.Show("Enter a number, please!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else //This block will work when any number is entered
k.
                {
                    int f_digit_of_whole = int.Parse(Number.Substring(0, 1)); //First digit of the number is assigned to the variable to be controlled

                    if (Number.Length > 1 && f_digit_of_whole == 0) //First digit of the entered number cannot be '0' (For e.g: 0123)
                    {
                        control = false;
                        MessageBox.Show("First digit of the entered number cannot be '0'!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lbl_text_of_number.Text = " "; //The label is cleaned.
                    }
                    else if (f_digit_of_whole == 0) //If the entererd number is only '0', the label will be written as zero.
                    {
                        control = false;
                        lbl_text_of_number.Text = "Sıfır TL "; // Sıfır means zero in Turkish. 
                    }
                    else
                    {
                        for (int i = 0; i < Number.Length; i++) // The loop will return the length of the variable.
                        {
                            control = Char.IsLetterOrDigit(Number[i]); //If there is no digit or letter in the i. digit of the variable, So there is a dot 'control' will be false.

                            if (control == false)
                            {
                                counter++; // The counter keeps the number of dots.
                            }
                        }
                        if (counter > 1) //If there is more than one dot, this cannot be a number, so control will be 'false'.
                        {
                            control = false;
                            MessageBox.Show("Enter a number in the correct form!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            lbl_text_of_number.Text = " "; //the label is cleaned.

                        }
                    }
                }

                if (control==true) //If the first digit of the entered number is any number that is not zero and the entered number is in the correct form, control will be 'true'. Therefore, further operations will occur.

                    for (int i = 0; i < Number.Length; i++) //// The loop will return the length of the variable.
                    {

                        control = Char.IsLetterOrDigit(Number[i]); //If there is a dot 'control' will be false
                        if (control == false)
                        {
                            break; //As soon as it finds the dot, it will break the loop and the 'control' will remain 'false'.
                        }

                    }

                    if (control == false) // If the 'control' is false, there is a dot in the number.
                    {
                        whole_place = Number.Split('.')[0]; //The part of the entered number up to the dot is taken with the 'Split[0]' method and assigned to the 'variable'

                        whole = int.Parse(whole_place); // 'The string 'whole_place' is converted to integer and assigned to the variable 'whole' int.

                        decimal_place = Number.Split('.')[1]; //The part of the entered number after the dot is taken with the 'Split[1]' method and assigned to the 'variable'.

                        int f_digit_of_decimal = int.Parse(decimal_place.Substring(0, 1)); // the first digit of the decimal part is taken 
                        decimal = int.Parse(decimal_place); //'The string 'whole_place' is converted to integer and assigned to the variable 'whole' int.

                        ////For e.g when entering 123.456, it will be 'whole'=123 and 'decimal'=456

                       if (decimal_place.Length>=2 && f_digit_of_decimal==0) //For e.g -> 12.'02'
                        {
                            decimal_part = false;
                        }

                        if (decimal >= 100) //If the Kurus(Cent) is more than 100, it converts to TL(Euro). The remainder remains in cents | For example: 150 kurus(Cent)= 1 TL(Euro) 50 Kurus(Cent)
                        {
                            int excess_part = decimal / 100;
                            whole += excess_part;
                            decimal = decimal % 100;
                            txt_number.Text = whole.ToString() + "." + decimal.ToString(); //The correct form of the amount is written in the textbox.
                        }

                    }
                    else if (control == true) // The 'control'is true, if there is no dot in the entered number.
                    {
                        whole = int.Parse(Number); // The string 'Number' is converted to integer and assigned to the 'whole' int variable.
                    }

                    if (whole >= 100000)
                    {
                        whole_part = false;
                        MessageBox.Show("You cannot enter more than 5 digits!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                    }
                    if (whole_part == true) // If the entered number is in the correct form, the bool is remain 'true'.
                    {
                        //These are the numbers written in Turkish.

                        int birler, onlar, yuzler, binler, on_binler; // In order of: {ones, tens, hundreds, thounsand, ten_thousands}
                        string[] bir = { " ", "Bir", "İki", "Üç", "Dört", "Beş", "Altı", "Yedi", "Sekiz", "Dokuz" }; //In order of: {one, two..., nine}
                        string[] on = { " ", "On", "Yirmi", "Otuz", "Kırk", "Elli", "Altmış", "Yetmiş", "Seksen", "Doksan" }; //In order of: ten,twenty...,ninety
                        string[] yüzler = { " ", "Yüz", "İki yüz", "Üç yüz", "Dört yüz", "Beş yüz", "Altı yüz", "Yedi yüz", "Sekiz yüz", "Dokuz yüz" }; //In order  of: {one hundred, two hundreds..., nine hundreds}
                        string[] onbin = { " ", "On bin", "Yirmi bin", "Otuz bin", "Kırk bin", "Elli bin", "Altmış bin", "Yetmiş bin", "Seksen bin", "Doksan bin" };
                        //In order of: {ten thousands, twenty thousands..., ninety thousands}

                        
                        string length_whole = whole.ToString(); //I converted the length of the int 'whole' to 'length_whole' to be used as a string.

                        on_binler = whole / 10000;

                        binler = (whole % 10000) / 1000;

                        yuzler = (whole % 1000) / 100;

                        onlar = (whole % 100) / 10;

                        birler = whole % 10;

                        if (length_whole.Length == 1 || length_whole.Length == 2)
                        {

                            lbl_text_of_number.Text = on[onlar] + " " + bir[birler] + " TL";
                        }

                        if (length_whole.Length == 3)
                        {

                            if (yuzler == 1)
                            {
                                lbl_text_of_number.Text = "Yüz " + on[onlar] + " " + bir[birler] + " TL";
                            }
                            else
                            {
                                lbl_text_of_number.Text = bir[yuzler] + " Yüz " + on[onlar] + " " + bir[birler] + " " + "Lira";

                            }
                        }
                        if (length_whole.Length == 4)
                        {
                            if (binler == 1)
                            {
                                lbl_text_of_number.Text = "Bin " + yüzler[yuzler] + " " + on[onlar] + " " + bir[birler] + " TL";
                            }
                            else
                                lbl_text_of_number.Text = bir[binler] + " Bin " + yüzler[yuzler] + " " + on[onlar] + " " + bir[birler] + " TL";
                        }
                        if (length_whole.Length == 5)
                        {

                            if (on_binler == 1)
                            {
                                lbl_text_of_number.Text = "On " + bir[binler] + " Bin " + yüzler[yuzler] + " " + on[onlar] + " " + bir[birler] + " TL";
                            }
                            else
                                lbl_text_of_number.Text = on[on_binler] + " " + bir[binler] + " Bin " + yüzler[yuzler] + " " + on[onlar] + " " + bir[birler] + " TL";

                        }

                        if (decimal != 0) //If the entered number has a decimal part, operations inside the block will occur. 
                        {                

                            if (decimal_part == true && decimal < 10)
                            {
                                decimal *= 10; //If a number such as 12.1 is entered, it will detect the 1 in the decimal part as 10.
                                txt_number.Text = whole.ToString() + "." + decimal.ToString(); // The correct form of the number is written in the textbox.
                            }

                            onlar = (decimal % 100) / 10;

                            birler = decimal % 10;

                            lbl_text_of_number.Text += ("  " + on[onlar] + " " + bir[birler] + " Kuruş");

                        }
                    }
                }
            }

            Button btn_exit = new Button(); //The button is created to exit.
            btn_exit.Text = "EXIT";
            btn_exit.Top = 150;
            btn_exit.Left = 220;
                 
            this.Controls.Add(btn_exit);
            btn_exit.Click += new EventHandler(Exit); //When the button is pressed, the operations of the 'Exit' function will be worked.
          
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Beige; //The background color of the form is changed as beige.
        }
        void Exit (object sender, EventArgs e)
        {
            DialogResult question = MessageBox.Show("Program will close", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Yes == question) //If the answer of the user is yes,
            {
                this.Close(); //The program will be closed.
            }
        }

    }
}
