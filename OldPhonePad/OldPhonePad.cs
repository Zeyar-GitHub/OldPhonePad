using System;
using System.Windows.Forms;

namespace OldPhonePad
{
    public partial class OldPhonePad : Form
    {
        private readonly string[] characters = { "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ" };

        public OldPhonePad()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string text = button.Tag.ToString();

            if (text == "#")
            {
                ricTextBox.Text = ProcessInput(ricTextBox.Text);
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

        private string ProcessInput(string inputText)
        {
            string charText = string.Empty;
            string result = string.Empty;

            foreach (var item in inputText)
            {
                if (string.IsNullOrEmpty(charText))
                {
                    if (!char.IsLetter(item))
                    {
                        charText = item.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else if (charText.Contains(item.ToString()))
                {
                    charText += item;
                }
                else if (item == '*')
                {
                    if (result.Length > 0)
                    {
                        result = result.Substring(0, result.Length - 1);
                        charText = string.Empty;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else if (char.IsWhiteSpace(item))
                {
                    result += ConvertToCharacter(charText);
                    charText = string.Empty;
                }
                else
                {
                    result += ConvertToCharacter(charText);
                    charText = item.ToString();
                }
            }

            if (!string.IsNullOrEmpty(charText))
            {
                result += ConvertToCharacter(charText);
            }

            return result;
        }

        private string ConvertToCharacter(string charText)
        {
            if (string.IsNullOrWhiteSpace(charText) || charText.Contains("*") || charText.Contains("1"))
                return string.Empty;

            int digit = int.Parse(charText[0].ToString());
            if (digit < 2 || digit > 9) return string.Empty;

            string charOptions = characters[digit - 2];
            int count = charText.Length;
            return count > charOptions.Length ? string.Empty : charOptions[count - 1].ToString();
        }
    }
}