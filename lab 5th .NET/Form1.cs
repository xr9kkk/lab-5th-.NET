using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TextFileProcessor
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.RichTextBox richTextBox1;
        private string currentFilePath = string.Empty;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Text File Processor";
        }

        private void InitializeComponent()
        {
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.processFileToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(800, 30);
            this.menuStrip2.TabIndex = 0;
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.newFileToolStripMenuItem.Text = "New";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.openFileToolStripMenuItem.Text = "Open";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.saveFileToolStripMenuItem.Text = "Save";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // processFileToolStripMenuItem
            // 
            this.processFileToolStripMenuItem.Name = "processFileToolStripMenuItem";
            this.processFileToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.processFileToolStripMenuItem.Text = "Process";
            this.processFileToolStripMenuItem.Click += new System.EventHandler(this.processFileToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // richTextBox1
            this.richTextBox1.Location = new System.Drawing.Point(0, 30);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(800, 420);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical; // добавляем вертикальную полосу прокрутки

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "Form1";
            this.Text = "Text File Processor";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Очищаем текстовое поле
            richTextBox1.Clear();

            // Вызываем диалог "Сохранить как" для нового файла
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Сохранить новый файл";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Сохраняем файл с указанным именем
                    File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
                }
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = openFileDialog.FileName;  // Update current file path
                    richTextBox1.Text = File.ReadAllText(currentFilePath);
                }
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                // If no file path, prompt user with Save As dialog
                SaveAs();
            }
            else
            {
                // Save to the existing file
                File.WriteAllText(currentFilePath, richTextBox1.Text);
                MessageBox.Show("File saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call SaveAs method to prompt user for file name
            SaveAs();
        }

        private void SaveAs()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Save As";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    File.WriteAllText(currentFilePath, richTextBox1.Text);
                    MessageBox.Show("File saved successfully!", "Save As", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void processFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получаем текущее содержимое RichTextBox
            var lines = richTextBox1.Lines;

            // Обрабатываем файл
            var processedLines = string.Join(" ", lines.Where(line => !string.IsNullOrWhiteSpace(line)));

            // Создаем диалоговое окно для выбора имени файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.Title = "Choose a file to save the processed content";

            // Предварительное сохранение результата
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                File.WriteAllText(fileName, processedLines);
                MessageBox.Show($"Processed content saved to {fileName}", "Success");
            }

            // Устанавливаем результат обработки в RichTextBox
            richTextBox1.Text = processedLines;
        }



        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
