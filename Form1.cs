using System.Drawing.Imaging;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    private const int boardSize = 4; // размер игрового поля
    private Button[,] buttonGrid; // массив кнопок для игрового поля
    private int[,] gameBoard; // массив для хранения текущего расположения квадратиков
    private Label count;
    
    public Form1()
    {
        
        InitializeComponent();
        cfgForm();
        InitializeGame();
        
    }
    public void cfgForm()
    {
        this.Size = new Size(500, 600); 
    }
    
    private void InitializeGame()
    {
        int buttonSize = 80; // размер кнопки
        int topPadding = (this.ClientSize.Height - buttonSize * boardSize) / 2; // отступ сверху (зависит от размера кнопок)
        int leftPadding = (this.ClientSize.Width - buttonSize * boardSize) / 2; // отступ слева (зависит от размера кнопок)
        
        // создаем игровое поле с кнопками
        buttonGrid = new Button[boardSize, boardSize];
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                buttonGrid[row, col] = new Button();
                buttonGrid[row, col].Size = new System.Drawing.Size(buttonSize, buttonSize);
                buttonGrid[row, col].Location = new System.Drawing.Point(leftPadding + col * buttonSize, topPadding + row * buttonSize); // формула для заполнения поля 
                buttonGrid[row, col].Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                buttonGrid[row, col].Click += new EventHandler(Button_Click); // устанавливает обработчик события Click
                // Button_Click: это имя метода, который будет вызываться при возникновении события
                buttonGrid[row, col].BackgroundImage = Image.FromFile("E:\\1\\jsegF14iwJs.jpg");
                this.Controls.Add(buttonGrid[row, col]);
            }
        }
        Button btnRestart = new Button();
        btnRestart.Width = Convert.ToInt32(buttonSize * 0.7);
        btnRestart.Height = Convert.ToInt32(buttonSize * 0.7);
        btnRestart.Text = "Reload";
        btnRestart.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        btnRestart.BackgroundImage = Image.FromFile("E:\\1\\jsegF14iwJs.jpg");
        btnRestart.Location = new Point( 0 , 0); // задайте координаты расположения кнопки на форме
        btnRestart.Click += new EventHandler(btnRestart_Click);
        this.Controls.Add(btnRestart);
        

        // создаем массив для хранения текущего расположения квадратиков ( от 1 до 15 )
        gameBoard = new int[boardSize, boardSize];
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                gameBoard[row, col] = row * boardSize + col + 1;
            }
        }
        gameBoard[boardSize - 1, boardSize - 1] = 0; // последний квадратик без цифры

        // перемешиваем квадратики в случайном порядке ( меняем расположение) 
        Random rand = new Random();
        for (int i = 0; i < 1000; i++)
        {
            int row1 = rand.Next(0, boardSize);
            int col1 = rand.Next(0, boardSize);
            int row2 = rand.Next(0, boardSize);
            int col2 = rand.Next(0, boardSize);
            int temp = gameBoard[row1, col1];
            gameBoard[row1, col1] = gameBoard[row2, col2];
            gameBoard[row2, col2] = temp;
        }

        // отображаем квадратики на кнопках
        UpdateButtons();
    }
   
    private void btnRestart_Click(object sender, EventArgs e)
    {
        // перемешиваем квадратики в случайном порядке ( меняем расположение) 
        Random rand = new Random();
        for (int i = 0; i < 1000; i++)
        {
            int row1 = rand.Next(0, boardSize);
            int col1 = rand.Next(0, boardSize);
            int row2 = rand.Next(0, boardSize);
            int col2 = rand.Next(0, boardSize);
            int temp = gameBoard[row1, col1];
            gameBoard[row1, col1] = gameBoard[row2, col2];
            gameBoard[row2, col2] = temp;
        }

        moves = 0;
        movesLabel.Text = "Moves: " + moves;

        // обновляем кнопки
        UpdateButtons();
    }
    private void UpdateButtons() // 0 помещает как пустую кнопку
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (gameBoard[row, col] == 0)
                {
                    buttonGrid[row, col].Text = "";
                    buttonGrid[row, col].Enabled = false;
                }
                else
                {
                    buttonGrid[row, col].Text = gameBoard[row, col].ToString();
                    buttonGrid[row, col].Enabled = true;
                }
            }
        }
        
    }
    
    private void Button_Click(object sender, EventArgs e)
    {
        // находим нажатую кнопку и её координаты на игровом поле
        Button clickedButton = (Button)sender;
        /*
        Код Button clickedButton = (Button)sender; приводит объект sender к типу Button и сохраняет его в переменную clickedButton.
        В событиях Windows Forms, sender является объектом, который вызывает событие. В данном случае, sender - 
        это объект, который был нажат и вызвал событие клика на кнопке. Чтобы получить доступ к свойствам и методам этой кнопки, 
        необходимо привести sender к типу Button.
        Таким образом, строка кода Button clickedButton = (Button)sender; позволяет получить доступ к свойствам и методам кнопки, которая была нажата.
        */
        int row = -1, col = -1;
        for (int r = 0; r < boardSize; r++)
        {
            for (int c = 0; c < boardSize; c++)
            {
                if (clickedButton == buttonGrid[r, c])
                {
                    row = r;
                    col = c;
                    break;
                }
            }
            if (row != -1) break;
        }

        // находим координаты пустой ячейки
        int emptyRow = -1, emptyCol = -1;
        for (int r = 0; r < boardSize; r++)
        {
            for (int c = 0; c < boardSize; c++)
            {
                if (gameBoard[r, c] == 0)
                {
                    emptyRow = r;
                    emptyCol = c;
                    break;
                }
            }
            if (emptyRow != -1) break;
        }

        // проверяем, можно ли передвинуть квадратик
        if ((row == emptyRow && Math.Abs(col - emptyCol) == 1) || (col == emptyCol && Math.Abs(row - emptyRow) == 1))
        {
            // передвигаем квадратик
            int temp = gameBoard[row, col];
            gameBoard[row, col] = gameBoard[emptyRow, emptyCol];
            gameBoard[emptyRow, emptyCol] = temp;
            
            

            // отображаем изменения на кнопках
            UpdateButtons();

            // проверяем, завершена ли игра
            if (IsGameCompleted())
            {
                MessageBox.Show("Вы победили!");
            }
        }
    }
    
    
    private bool IsGameCompleted()
    {
        int counter = 1;
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (gameBoard[row, col] != counter)
                {
                    return false;
                }
                counter++;
                if (counter == boardSize * boardSize)
                {
                    break;
                }
            }
            if (counter == boardSize * boardSize)
            {
                break;
            }
        }
        return true;
    }
    
}
    
    
