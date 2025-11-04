using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public static  class NavigationManager
    {
        private static Stack<Form> formHistory = new Stack<Form>();

        
        public static void NavigateTo(Form nextForm, Form currentForm)
        {
            formHistory.Push(currentForm); // Save the current form
            nextForm.Show();
            currentForm.Close();
        }

        
        public static void GoBack(Form currentForm)
        {
            if (formHistory.Count > 0)
            {
                Form previousForm = formHistory.Pop(); 
                previousForm.Show();
                currentForm.Close();
            }
            else
            {
                MessageBox.Show("No previous form to go back to.", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

