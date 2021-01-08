using System;

namespace TA2
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }      

        private void button1_Click(object sender, EventArgs e)
        {
            LexemeManager lexemeManager = new LexemeManager();
            dataGridView1.DataSource = lexemeManager.getLexemesTable(textBox1.Text);
        }
    }
}
