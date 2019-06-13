//Morghan Kiverago HangMan Code 14/6/2019
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int count = 0;//Variable For Counting the number of letters in a selected word
        string RightAnswer; //String That represents the Correct answer
        Random random = new Random(); // initializing random to later be used to select a random word from the Words.txt File
        string HiddenAnswer; //String that represents The Hidden Answer
        string[] incorrectGuessed = new string[7]; 
        string[] WORDS = new string[100]; //Words represents the number that was selected for the random word. for example if it is 5 it will select the word on line 5 on the words.txt file
        int counter = 7; //Variable for counting the number of lives
        string lblWrong = null;
        Rectangle PlatFormUp;
        System.IO.StreamReader WordList = new System.IO.StreamReader("Words.txt"); //sets up streamreader to read words.txt
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            counter = 7;
            PlatFormUp = new Rectangle();
            PlatFormUp.Width = 70;
            PlatFormUp.Height = 5;
            PlatFormUp.Fill = Brushes.Black;
            HangMan.Children.Add(PlatFormUp);
            Canvas.SetTop(PlatFormUp, 404);
            Canvas.SetLeft(PlatFormUp, 400);

            count = 0;
            int randomnumber = random.Next(1, 101); //selects random number between 1-100 to pick a word from words.txt
            lblOutput.Text = ""; 
                while (!WordList.EndOfStream) //when it's at end of stream it will figure out what word and how many letters are in it to be displayed
                {
                    if (count == randomnumber)
                    {
                        WORDS[randomnumber] = WordList.ReadLine();
                    }
                    else
                    {
                        WordList.ReadLine();
                    }
                    RightAnswer = WORDS[randomnumber];
                    count++;
                
                }

                for (int i = 0; i < RightAnswer.Length; i++)//Displays the hidden word by running a loop in which for every letter there is a _ and for every letter there is a space after it
                {
                    lblOutput.Text += "_" + " ";
                }
            MessageBox.Show("Delete The Previous Letter To Type A New One");
            RightAnswer.ToUpper();
        }

        private void btnLetterCheck_Click(object sender, RoutedEventArgs e)
        {
            //replace the letter of the input if right
            HiddenAnswer = lblOutput.Text.ToString();
            if (RightAnswer == null)
            {
                MessageBox.Show("Press New Game To Play Again");
                RightAnswer = " ";
            }
            else
            {
                //To Make Sure People Don't try to Break The Game And Type Things They Shouldn't This Will Check If Any Illeagle Characters Are Used
                if (txtLetterInput.Text == "`" || txtLetterInput.Text == "!" || txtLetterInput.Text == "@" || txtLetterInput.Text == "#" || txtLetterInput.Text == "$" || txtLetterInput.Text == "%"
                    || txtLetterInput.Text == "^" || txtLetterInput.Text == "&" || txtLetterInput.Text == "*" || txtLetterInput.Text == "(" || txtLetterInput.Text == ")" || txtLetterInput.Text == "-"
                    || txtLetterInput.Text == "_" || txtLetterInput.Text == "1" || txtLetterInput.Text == "2" || txtLetterInput.Text == "3" || txtLetterInput.Text == "4" || txtLetterInput.Text == "5"
                    || txtLetterInput.Text == "6" || txtLetterInput.Text == "7" || txtLetterInput.Text == "8" || txtLetterInput.Text == "9" || txtLetterInput.Text == "=" || txtLetterInput.Text == "+"
                    || txtLetterInput.Text == "[" || txtLetterInput.Text == "{" || txtLetterInput.Text == "]" || txtLetterInput.Text == "}" || txtLetterInput.Text == "|" || txtLetterInput.Text == ";"
                    || txtLetterInput.Text == ":" || txtLetterInput.Text == "'" || txtLetterInput.Text == "\"" //Olivershowed me what an escape character is so I can make sure nobody tries to type "
                    || txtLetterInput.Text == "," || txtLetterInput.Text == "<" || txtLetterInput.Text == "." || txtLetterInput.Text == ">" || txtLetterInput.Text == "/" || txtLetterInput.Text == "?")
                    txtLetterInput.Clear();
                if (!RightAnswer.Contains(txtLetterInput.Text))//if you guess wrong it subtracts one life from the counter
                {
                    counter--;
                    //incorrectGuessed[i] = txtLetterInput.Text;
                    lblWrong = lblWrong + txtLetterInput.Text + " ";
                    lblWrongLetter.Content = lblWrong;
                    if (counter != 0) //runs this to tell you that you are wrong
                    {
                        lblWrongLetter.Content = "Your Attempt At Salvation :  " + "\"" + txtLetterInput.Text + "\"" + " was incorrect!";
                        lblLives.Content = Environment.NewLine + "Lives left:" + counter.ToString();
                    }
                    if (counter == 0)
                    {
                        lblOutput.Text = "I Diagnose You With Dead. The word you were looking for was: " + RightAnswer;
                    }
                    txtLetterInput.Clear();
                }
                else
                {
                    for (int i = 0; i < RightAnswer.Length; i++) // If you manage to get a letter correct this will run 
                    {
                        char lettersingle = RightAnswer[i];
                        if (lettersingle.ToString() == txtLetterInput.Text)
                        {
                            HiddenAnswer = HiddenAnswer.Remove(i * 2, 1); //This removes _ of the correct letter guessed 
                            HiddenAnswer = HiddenAnswer.Insert(i * 2, lettersingle.ToString()); //this replaces the _ with the letter
                            lblOutput.Text = "";
                            lblOutput.Text += HiddenAnswer;
                        }
                    }
                    txtLetterInput.Clear();//clears letter from txtinput
                }
                if (!HiddenAnswer.Contains("_"))
                {
                    MessageBox.Show("Good For You, You Live Another Day But This Purgatory Will Continue \n The correct word was: " + RightAnswer.ToUpper());
                }
                if (counter == 6)
                {
                    Ellipse head = new Ellipse();
                    head.Width = 60;
                    head.Height = 60;
                    head.Fill = Brushes.Black;
                    head.Stroke = Brushes.Black;
                    head.StrokeThickness = 5;
                    HangMan.Children.Add(head);
                    Canvas.SetTop(head, 232);
                    Canvas.SetLeft(head, 408);
                }
                else if (counter == 5)
                {
                    Rectangle Body = new Rectangle();
                    Body.Width = 5;
                    Body.Height = 70;
                    Body.Fill = Brushes.Black;
                    Body.Stroke = Brushes.Black;
                    HangMan.Children.Add(Body);
                    Canvas.SetTop(Body, 287);
                    Canvas.SetLeft(Body, 436);
                }
                else if (counter == 4)
                {
                    Line leg1 = new Line();
                    leg1.X1 = 438;
                    leg1.X2 = 460;
                    leg1.Y1 = 355;
                    leg1.Y2 = 400;
                    leg1.Stroke = Brushes.Black;
                    leg1.StrokeThickness = 5;
                    leg1.HorizontalAlignment = HorizontalAlignment.Left;
                    leg1.VerticalAlignment = VerticalAlignment.Center;
                    HangMan.Children.Add(leg1);
                }
                else if (counter == 3)
                {
                    Line leg2 = new Line();
                    leg2.X1 = 438;
                    leg2.X2 = 416;
                    leg2.Y1 = 295;
                    leg2.Y2 = 340;
                    leg2.Stroke = Brushes.Black;
                    leg2.StrokeThickness = 5;
                    leg2.HorizontalAlignment = HorizontalAlignment.Left;
                    leg2.VerticalAlignment = VerticalAlignment.Center;
                    HangMan.Children.Add(leg2);
                }
                else if (counter == 2)
                {
                    Line arm1 = new Line();
                    arm1.X1 = 438;
                    arm1.X2 = 460;
                    arm1.Y1 = 295;
                    arm1.Y2 = 340;
                    arm1.Stroke = Brushes.Black;
                    arm1.StrokeThickness = 5;
                    arm1.HorizontalAlignment = HorizontalAlignment.Left;
                    arm1.VerticalAlignment = VerticalAlignment.Center;
                    HangMan.Children.Add(arm1);
                }
                else if (counter == 1)
                {
                    Line arm2 = new Line();
                    arm2.X1 = 438;
                    arm2.X2 = 416;
                    arm2.Y1 = 355;
                    arm2.Y2 = 400;
                    arm2.Stroke = Brushes.Black;
                    arm2.StrokeThickness = 5;
                    arm2.HorizontalAlignment = HorizontalAlignment.Left;
                    arm2.VerticalAlignment = VerticalAlignment.Center;
                    HangMan.Children.Add(arm2);
                }
                else if (counter == 0)
                {
                    HangMan.Children.Remove(PlatFormUp);
                    Rectangle PlatFormDown = new Rectangle();
                    PlatFormDown.Width = 5;
                    PlatFormDown.Height = 70;
                    PlatFormDown.Fill = Brushes.Black;
                    HangMan.Children.Add(PlatFormDown);
                    Canvas.SetTop(PlatFormDown, 404);
                    Canvas.SetLeft(PlatFormDown, 400);
                    HangMan.Children.Clear();
                    Ellipse head = new Ellipse();
                    head.Width = 60;
                    head.Height = 60;
                    head.Fill = Brushes.Black;
                    head.Stroke = Brushes.Black;
                    head.StrokeThickness = 5;
                    HangMan.Children.Add(head);
                    Canvas.SetTop(head, 252);
                    Canvas.SetLeft(head, 408);

                    Rectangle Body = new Rectangle();
                    Body.Width = 5;
                    Body.Height = 70;
                    Body.Fill = Brushes.Black;
                    Body.Stroke = Brushes.Black;
                    HangMan.Children.Add(Body);
                    Canvas.SetTop(Body, 307);
                    Canvas.SetLeft(Body, 436);

                    Line leg1 = new Line();
                    leg1.X1 = 438;
                    leg1.X2 = 460;
                    leg1.Y1 = 375;
                    leg1.Y2 = 420;
                    leg1.Stroke = Brushes.Black;
                    leg1.StrokeThickness = 5;
                    leg1.HorizontalAlignment = HorizontalAlignment.Left;
                    leg1.VerticalAlignment = VerticalAlignment.Center;
                    HangMan.Children.Add(leg1);

                    Line leg2 = new Line();
                    leg2.X1 = 438;
                    leg2.X2 = 416;
                    leg2.Y1 = 315;
                    leg2.Y2 = 360;
                    leg2.Stroke = Brushes.Black;
                    leg2.StrokeThickness = 5;
                    leg2.HorizontalAlignment = HorizontalAlignment.Left;
                    leg2.VerticalAlignment = VerticalAlignment.Center;
                    HangMan.Children.Add(leg2);

                    Line arm1 = new Line();
                    arm1.X1 = 438;
                    arm1.X2 = 460;
                    arm1.Y1 = 315;
                    arm1.Y2 = 360;
                    arm1.Stroke = Brushes.Black;
                    arm1.StrokeThickness = 5;
                    arm1.HorizontalAlignment = HorizontalAlignment.Left;
                    arm1.VerticalAlignment = VerticalAlignment.Center;
                    HangMan.Children.Add(arm1);

                    Line arm2 = new Line();
                    arm2.X1 = 438;
                    arm2.X2 = 416;
                    arm2.Y1 = 375;
                    arm2.Y2 = 420;
                    arm2.Stroke = Brushes.Black;
                    arm2.StrokeThickness = 5;
                    arm2.HorizontalAlignment = HorizontalAlignment.Left;
                    arm2.VerticalAlignment = VerticalAlignment.Center;
                    HangMan.Children.Add(arm2);
                }
            }
            txtLetterInput.Focus();
        }

        private void BtnReplay_Click(object sender, RoutedEventArgs e)
        {
            counter = 7;
            WordList.DiscardBufferedData();
            WordList.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            HangMan.Children.Clear();
            lblLetters.Content = " ";
            lblWrongLetter.Content = " ";

            for (int i = 0; i < 10; i++)
            {
                RightAnswer = null;
            }
            lblOutput.Text = "";
            counter = 5;
            lblWrong = null;
        }

        private void Surprise_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=0WtixLg56Y8");
        }
    }
}