using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace OldPhonePad
{
    public partial class OldPhonePad : Form
    {
        private string[] characters = { "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ" };

        private string alpha = "";
        private int clickCount = 0;
        private Timer tm;


        public OldPhonePad()
        {
            InitializeComponent();
            tm = new Timer();
            tm.Interval = 250;
            tm.Tick += Tm_Tick;
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            if (clickCount > alpha.Length) clickCount = alpha.Length;
            char[] charArrs = alpha.ToCharArray();
            ricTextBox.AppendText(charArrs[clickCount - 1].ToString());
            tm.Stop();
            clickCount = 0;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string text = button.Tag.ToString();
            if (text == "#")
            {
                string inputText = ricTextBox.Text.ToString();

                string charText = string.Empty;
                string result = string.Empty;


                foreach (var item in inputText)
                {
                    if (string.IsNullOrEmpty(charText))
                    {
                        if (!string.IsNullOrEmpty(item.ToString()) && !char.IsLetter(item))
                        {
                            charText = item.ToString();
                        }
                        else
                        {
                            ricTextBox.Clear();
                            break;
                        }
                    }
                    else if (charText.Contains(item.ToString()))
                    {
                        charText += item;
                    }
                    else if (item.ToString().Contains("*"))
                    {
                        result += ChangeCharacter(charText);
                        if (result.Length != 0)
                        {
                            result = result.Substring(0, result.Length - 1);
                            charText = string.Empty;
                        }
                        else
                        {
                            ricTextBox.Clear();
                            break;
                        }
                    }
                    else if (Char.IsWhiteSpace(item))
                    {
                        result += ChangeCharacter(charText);
                        charText = string.Empty;
                    }
                    else
                    {
                        result += ChangeCharacter(charText);
                        charText = item.ToString();
                    }
                }

                if (!string.IsNullOrEmpty(charText))
                {
                    result += ChangeCharacter(charText);
                }

                ricTextBox.Text = result;
            }
            else if (text == "0")
            {
                ricTextBox.AppendText(" ");
            }
            else
            {
                ricTextBox.AppendText(text);
            }
        }

        private string ChangeCharacter(string charText)
        {
            if (charText.Contains("*") || charText.Contains("1")) return string.Empty;
            if (!string.IsNullOrEmpty(charText) && !string.IsNullOrWhiteSpace(charText))
            {
                int alpha = int.Parse(charText[0].ToString());
                int count = charText.Length;
                string charArrs = characters[alpha - 2];
                if (count > charArrs.Length)
                {
                    ricTextBox.Clear();
                    return string.Empty;
                }
                else
                {
                    return charArrs[count - 1].ToString();
                }
            }
            else { return string.Empty; }
        }
    }
}
