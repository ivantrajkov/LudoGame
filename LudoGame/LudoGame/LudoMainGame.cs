using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LudoGame
{
    public partial class LudoMainGame : Form
    {
        int numOfPlayers = 0;
        bool diceRolled = false;
        int diceValue = 0;
        Player yellow;
        Player blue;
        Player green;
        Player red;
        List<Player> players = new List<Player>();
        int currentPlayerIndex = 0;
        PictureBox[] pictureBoxes = new PictureBox[64];
        bool moved = false;
        SoundPlayer music;
        List<PictureBox> allHomes;

        public LudoMainGame(int n)
        {
            InitializeComponent();

            AttachClickEvents();
            numOfPlayers = n;
            timer1.Start();

            this.Height = this.ClientSize.Height + 100;

            start();
            string sPath = @"Assets\gameMusic.wav";
            music = new SoundPlayer(sPath);
            music.PlayLooping();

        }
        private void loadPlayer(Player player, List<PictureBox> homes, string color, string imagePath)
        {
            player = new Player(color);
            for (int i = 0; i < homes.Count; i++)
            {
                Piece piece = new Piece(color);
                player.pieces.Add(piece);
                homes[i].Tag = piece;
                homes[i].Image = Image.FromFile(imagePath);
            }
            players.Add(player);
        }

        public void start()
        {
            allHomes = new List<PictureBox>
        {
            yellowHome1, yellowHome2, yellowHome3, yellowHome4,
            blueHome1, blueHome2, blueHome3, blueHome4,
            redHome1, redHome2, redHome3, redHome4,
            greenHome1, greenHome2, greenHome3, greenHome4,

        };


            attachHomeClickEvents();
            if (numOfPlayers == 4)
            {
                string yellowImagePath = @"Assets\yellowTilePiece.png";
                List<PictureBox> yellowHomes = new List<PictureBox> { yellowHome1, yellowHome2, yellowHome3, yellowHome4 };
                loadPlayer(yellow, yellowHomes, "yellow", yellowImagePath);

                string blueImagePath = @"Assets\blueTilePiece.png";
                List<PictureBox> blueHomes = new List<PictureBox> { blueHome1, blueHome2, blueHome3, blueHome4 };
                loadPlayer(blue, blueHomes, "blue", blueImagePath);

                string redImagePath = @"Assets\redTilePiece.png";
                List<PictureBox> redHomes = new List<PictureBox> { redHome1, redHome2, redHome3, redHome4 };
                loadPlayer(red, redHomes, "red", redImagePath);

                string greenImagePath = @"Assets\greenTilePiece.png";
                List<PictureBox> greenHomes = new List<PictureBox> { greenHome1, greenHome2, greenHome3, greenHome4 };
                loadPlayer(green, greenHomes, "green", greenImagePath);


            } else if(numOfPlayers == 3)
            {
                string yellowImagePath = @"Assets\yellowTilePiece.png";
                List<PictureBox> yellowHomes = new List<PictureBox> { yellowHome1, yellowHome2, yellowHome3, yellowHome4 };
                loadPlayer(yellow, yellowHomes, "yellow", yellowImagePath);

                string blueImagePath = @"Assets\blueTilePiece.png";
                List<PictureBox> blueHomes = new List<PictureBox> { blueHome1, blueHome2, blueHome3, blueHome4 };
                loadPlayer(blue, blueHomes, "blue", blueImagePath);

                string redImagePath = @"Assets\redTilePiece.png";
                List<PictureBox> redHomes = new List<PictureBox> { redHome1, redHome2, redHome3, redHome4 };
                loadPlayer(red, redHomes, "red", redImagePath);

            } else if(numOfPlayers == 2)
            {
                string yellowImagePath = @"Assets\yellowTilePiece.png";
                List<PictureBox> yellowHomes = new List<PictureBox> { yellowHome1, yellowHome2, yellowHome3, yellowHome4 };
                loadPlayer(yellow, yellowHomes, "yellow", yellowImagePath);

                string blueImagePath = @"Assets\blueTilePiece.png";
                List<PictureBox> blueHomes = new List<PictureBox> { blueHome1, blueHome2, blueHome3, blueHome4 };
                loadPlayer(blue, blueHomes, "blue", blueImagePath);
            }

            updateUI();
        }


        private void AttachClickEvents()
        {
            List<PictureBox> pictureBoxList = new List<PictureBox>();

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox pictureBox && control.Name.StartsWith("position"))
                {
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                    control.Click -= PictureBox_Click;

                    control.Click += new EventHandler(PictureBox_Click);

                    pictureBoxList.Add(pictureBox);
                }
            }
            pictureBoxes = pictureBoxList.ToArray();
        }


        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (!diceRolled)
            {
                MessageBox.Show("Roll the dice first!");
                return;
            }

            PictureBox clickedPictureBox = (PictureBox)sender;
            if (clickedPictureBox.Tag != null)
            {
                Piece piece = clickedPictureBox.Tag as Piece;
                Player currentPlayer = players[currentPlayerIndex];

                if (currentPlayer.name != piece.color)
                {
                    return;
                }


                int currentIndex = int.Parse(clickedPictureBox.Name.Substring(piece.isFinished ? 9 : 8));
                int newIndex = currentIndex + diceValue;
                if ((currentPlayer.name == "blue" || currentPlayer.name == "red" || currentPlayer.name == "green") && newIndex > 48)
                {
                    newIndex = 0;
                    newIndex = currentIndex + diceValue;
                    int position = newIndex % 48;
                    string newPositionName = "position" + position;
                    string rp;
                    PictureBox newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;
                    if(newPositionPictureBox.Tag != null)
                    {
                        Piece tmpPiece = newPositionPictureBox.Tag as Piece;
                        if (tmpPiece.color == currentPlayer.name)
                        {
                            return;
                        }
                        else
                        {
                            newPositionPictureBox.Tag = null;
                            PictureBox home = checkHome(tmpPiece.color);
                            tmpPiece.passedHalf = false;
                            home.Tag = tmpPiece;
                            tmpPiece.isAtHome = true;
                            rp = @"Assets/" + tmpPiece.color + "TilePiece.png";
                            home.Image = Image.FromFile(rp);


                        }
                    }
                    rp = @"Assets/"+currentPlayer.name+"Piece.png";
                    newPositionPictureBox.Image = Image.FromFile(rp);
                    newPositionPictureBox.Tag = piece;
                    clickedPictureBox.Tag = null;
                    clickedPictureBox.Image = null;
                    piece.passedHalf = true;               
                }
                else if (currentPlayer.name == "yellow" && newIndex > 48)
                {
                    int position = newIndex % 48;
                    string newPositionName = "positionY" + position;
                   // currentPlayer.background = newPositionName;

                    PictureBox newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;

                    if (newPositionPictureBox != null)
                    {
                        if (newPositionPictureBox.Tag != null)
                        {
                            return;
                        }
                        newPositionPictureBox.Tag = piece;
                        string pieceImagePath = @"Assets\" + piece.color + "TilePiece.png";
                        newPositionPictureBox.Image = Image.FromFile(pieceImagePath);
                        clickedPictureBox.Tag = null;
                        clickedPictureBox.Image = null;
                        piece.isFinished = true;
                    }

                }
                else if (currentPlayer.name == "yellow" && piece.isFinished)
                {
                    currentIndex = int.Parse(clickedPictureBox.Name.Substring(9));
                    newIndex = currentIndex + diceValue;
                    if ((currentIndex + diceValue) > 4)
                    {
                        return;
                    }
                    // string newPositionName = "position" + "Yellow" + newIndex;
                    string newPositionName = "positionY" + newIndex;
                    PictureBox newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;
                    if (newPositionPictureBox.Tag != null)
                    {
                        return;
                    }
                    newPositionName = "positionY" + newIndex;
                    newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;
                    newPositionPictureBox.Tag = piece;
                    string pieceImagePath = @"Assets\" + piece.color + "TilePiece.png";
                    newPositionPictureBox.Image = Image.FromFile(pieceImagePath);
                    clickedPictureBox.Tag = null;
                    pieceImagePath = @"Assets\" + piece.color + "Tile.png";
                    clickedPictureBox.Image = Image.FromFile(pieceImagePath);

                }
                else if (currentPlayer.name == "blue" && piece.passedHalf == true && newIndex > 12)
                {
                    int position = newIndex % 12;
                    string newPositionName = "positionB" + position;
                    //currentPlayer.background = newPositionName;

                    PictureBox newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;

                    if (newPositionPictureBox != null)
                    {
                        if (newPositionPictureBox.Tag != null)
                        {
                            return;
                        }
                        newPositionPictureBox.Tag = piece;
                        string pieceImagePath = @"Assets\" + piece.color + "TilePiece.png";
                        newPositionPictureBox.Image = Image.FromFile(pieceImagePath);
                        clickedPictureBox.Tag = null;
                        clickedPictureBox.Image = null;
                        piece.isFinished = true;
                    }
                }
                else if (currentPlayer.name == "blue" && piece.isFinished)
                {
                    currentIndex = int.Parse(clickedPictureBox.Name.Substring(9));
                    newIndex = currentIndex + diceValue;
                    if ((currentIndex + diceValue) > 4)
                    {
                        return;
                    }
                    // string newPositionName = "position" + "Yellow" + newIndex;
                    string newPositionName = "positionB" + newIndex;
                    PictureBox newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;
                    if (newPositionPictureBox.Tag != null)
                    {
                        return;
                    }
                    newPositionName = "positionB" + newIndex;
                    newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;
                    newPositionPictureBox.Tag = piece;
                    string pieceImagePath = @"Assets\" + piece.color + "TilePiece.png";
                    newPositionPictureBox.Image = Image.FromFile(pieceImagePath);
                    clickedPictureBox.Tag = null;
                    pieceImagePath = @"Assets\" + piece.color + "Tile.png";
                    clickedPictureBox.Image = Image.FromFile(pieceImagePath);

                }
                else if (currentPlayer.name == "red" && piece.passedHalf == true && newIndex > 24)
                {
                    int position = newIndex % 24;
                    string newPositionName = "positionR" + position;
                   // currentPlayer.background = newPositionName;

                    PictureBox newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;

                    if (newPositionPictureBox != null)
                    {
                        if (newPositionPictureBox.Tag != null)
                        {
                            return;
                        }
                        newPositionPictureBox.Tag = piece;
                        string pieceImagePath = @"Assets\" + piece.color + "TilePiece.png";
                        newPositionPictureBox.Image = Image.FromFile(pieceImagePath);
                        clickedPictureBox.Tag = null;
                        clickedPictureBox.Image = null;
                        piece.isFinished = true;
                    }
                }
                else if (currentPlayer.name == "red" && piece.isFinished)
                {
                    currentIndex = int.Parse(clickedPictureBox.Name.Substring(9));
                    newIndex = currentIndex + diceValue;
                    if ((currentIndex + diceValue) > 4)
                    {
                        return;
                    }
                    
                    string newPositionName = "positionR" + newIndex;
                    PictureBox newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;
                    if (newPositionPictureBox.Tag != null)
                    {
                        return;
                    }
                    newPositionName = "positionR" + newIndex;
                    newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;
                    newPositionPictureBox.Tag = piece;
                    string pieceImagePath = @"Assets\" + piece.color + "TilePiece.png";
                    newPositionPictureBox.Image = Image.FromFile(pieceImagePath);
                    clickedPictureBox.Tag = null;
                    pieceImagePath = @"Assets\" + piece.color + "Tile.png";
                    clickedPictureBox.Image = Image.FromFile(pieceImagePath);

                }
                else if (currentPlayer.name == "green" && piece.passedHalf == true && newIndex > 36)
                {
                    int position = newIndex % 36;
                    string newPositionName = "positionG" + position;
                   // currentPlayer.background = newPositionName;

                    PictureBox newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;

                    if (newPositionPictureBox != null)
                    {
                        if (newPositionPictureBox.Tag != null)
                        {
                            return;
                        }
                        newPositionPictureBox.Tag = piece;
                        string pieceImagePath = @"Assets\" + piece.color + "TilePiece.png";
                        newPositionPictureBox.Image = Image.FromFile(pieceImagePath);
                        clickedPictureBox.Tag = null;
                        clickedPictureBox.Image = null;
                        piece.isFinished = true;
                    }
                }
                else if (currentPlayer.name == "green" && piece.isFinished)
                {
                    currentIndex = int.Parse(clickedPictureBox.Name.Substring(9));
                    newIndex = currentIndex + diceValue;
                    if ((currentIndex + diceValue) > 4)
                    {
                        return;
                    }
                    // string newPositionName = "position" + "Yellow" + newIndex;
                    string newPositionName = "positionG" + newIndex;
                    PictureBox newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;
                    if (newPositionPictureBox.Tag != null)
                    {
                        return;
                    }
                    newPositionName = "positionG" + newIndex;
                    newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;
                    newPositionPictureBox.Tag = piece;
                    string pieceImagePath = @"Assets\" + piece.color + "TilePiece.png";
                    newPositionPictureBox.Image = Image.FromFile(pieceImagePath);
                    clickedPictureBox.Tag = null;
                    pieceImagePath = @"Assets\" + piece.color + "Tile.png";
                    clickedPictureBox.Image = Image.FromFile(pieceImagePath);
                }



                else
                {
                    string newPositionName = "position" + newIndex;
                    PictureBox newPositionPictureBox = this.Controls.Find(newPositionName, true).FirstOrDefault() as PictureBox;
                    if (newPositionPictureBox.Tag != null)
                    {
                        Piece tmpPiece = newPositionPictureBox.Tag as Piece;
                        if (tmpPiece.color == currentPlayer.name)
                        {
                            return;
                        }
                        else
                        {
                            newPositionPictureBox.Tag = null;
                            PictureBox home = checkHome(tmpPiece.color);
                            home.Tag = tmpPiece;
                            tmpPiece.isAtHome = true;
                            tmpPiece.passedHalf = false;
                            string rp = @"Assets/" + tmpPiece.color + "TilePiece.png";
                            home.Image = Image.FromFile(rp);


                        }
                    }
                    if (newPositionPictureBox != null)
                    {
                        newPositionPictureBox.Tag = piece;
                        string pieceImagePath = @"Assets\" + piece.color + "Piece.png";
                        newPositionPictureBox.Image = Image.FromFile(pieceImagePath);
                    }

                    clickedPictureBox.Tag = null;
                    clickedPictureBox.Image = null;
                }


                if (diceValue != 6)
                {
                    foreach (PictureBox pb in pictureBoxes)
                    {
                        pb.Click -= PictureBox_Click;

                    }
                    moved = true;
                }
                diceRolled = false;
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LudoMainGame_Load(object sender, EventArgs e)
        {

        }
        public async void diceRoll()
        {
            if (diceRolled)
            {
                MessageBox.Show("Dice already rolled!");
                return;
            }
            Random random = new Random();

            string sPath = @"Assets\roll.wav";
            SoundPlayer player = new SoundPlayer(sPath);

            player.Play();
            diceRolled = true;
            int n;
            for (int i = 0; i < 6; i++)
            {
                n = random.Next(1, 7);
                await Task.Delay(50);
                diceValue = n;
                if (n == 1)
                {
                    string relativePath = @"Assets\dice1.png";

                    dice.Image = Image.FromFile(relativePath);
                }
                if (n == 2)
                {
                    string relativePath = @"Assets\dice2.png";

                    dice.Image = Image.FromFile(relativePath);
                }
                if (n == 3)
                {
                    string relativePath = @"Assets\dice3.png";

                    dice.Image = Image.FromFile(relativePath);
                }
                if (n == 4)
                {
                    string relativePath = @"Assets\dice4.png";

                    dice.Image = Image.FromFile(relativePath);

                }
                if (n == 5)
                {
                    string relativePath = @"Assets\dice5.png";

                    dice.Image = Image.FromFile(relativePath);
                }
                if (n == 6)
                {
                    string relativePath = @"Assets\dice6.png";

                    dice.Image = Image.FromFile(relativePath);
                }
            }

            music.PlayLooping();
            if (diceRolled)
            {
                Player currentPlayer = players[currentPlayerIndex];

                bool allPiecesAtHome = currentPlayer.pieces.All(piece => piece.isAtHome);
                bool pieceAtEnd = currentPlayer.pieces.Any(piece => piece.isFinished);


                if ((allPiecesAtHome && diceValue != 6) || (pieceAtEnd && diceValue != 6))
                {
                    moved = true;
                    return;
                }
            }


        }

        private void pictureBox69_Click(object sender, EventArgs e)
        {
            diceRoll();


        }

        public void attachHomeClickEvents()
        {
            List<PictureBox> homePictureBoxes = new List<PictureBox>
                {
                     yellowHome1, yellowHome2, yellowHome3, yellowHome4,
                     //blueHome1, blueHome2, blueHome3, blueHome4
                };

            foreach (PictureBox pb in homePictureBoxes)
            {
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Click += new EventHandler(yellowHome_Click);
            }

            homePictureBoxes = new List<PictureBox> {
                    blueHome1, blueHome2, blueHome3, blueHome4
                };
            foreach (PictureBox pb in homePictureBoxes)
            {
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Click += new EventHandler(blueHome_Click);
            }
            homePictureBoxes = new List<PictureBox> {
                    greenHome1, greenHome2, greenHome3, greenHome4
                };
            foreach (PictureBox pb in homePictureBoxes)
            {
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Click += new EventHandler(greenHome_Click);
            }

            homePictureBoxes = new List<PictureBox> {
                    redHome1, redHome2, redHome3, redHome4
                };
            foreach (PictureBox pb in homePictureBoxes)
            {
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Click += new EventHandler(redHome_Click);
            }
        }

        private void yellowHome_Click(object sender, EventArgs e)
        {
            if (!diceRolled)
            {
                MessageBox.Show("Roll the dice first!");
                return;

            }
            else if (diceValue != 6 || moved)
            {
                return;
            }
            PictureBox p = (PictureBox)sender;
            if (p.Tag != null)
            {
                Piece piece = p.Tag as Piece;
                if (players[currentPlayerIndex].name != piece.color)
                    return;
                if (position1.Tag != null)
                {
                    piece = position1.Tag as Piece;
                    if(piece.color == players[currentPlayerIndex].name)
                    {
                        return;
                    }
                    PictureBox home = checkHome(piece.color);
                    home.Tag = piece;
                    piece.isAtHome = true;
                    string rp = @"Assets/" + piece.color + "TilePiece.png";
                    home.Image = Image.FromFile(rp);

                    piece = p.Tag as Piece;
                    position1.Tag = piece;
                    piece.isAtHome = false;
                    rp = @"Assets\yellowPiece.png";
                    position1.Image = Image.FromFile(rp);

                    rp = @"Assets\yellowTile.png";
                    p.Image = Image.FromFile(rp);
                    p.Tag = null;
                    moved = false;
                    diceRolled = false;
                }
                else
                {
                    piece = p.Tag as Piece;
                    position1.Tag = piece;
                    piece.isAtHome = false;
                    string rp = @"Assets\yellowPiece.png";
                    position1.Image = Image.FromFile(rp);

                    rp = @"Assets\yellowTile.png";
                    p.Image = Image.FromFile(rp);
                    p.Tag = null;
                    moved = false;
                    diceRolled = false;
                }
            }
        }

        private void blueHome_Click(object sender, EventArgs e)
        {
            if (!diceRolled)
            {
                MessageBox.Show("Roll the dice first!");
                return;
            }
            else if (diceValue != 6 || moved)
            {
                return;
            }
            PictureBox p = (PictureBox)sender;
            if (p.Tag != null)
            {
                Piece piece = p.Tag as Piece;
                if (players[currentPlayerIndex].name != piece.color)
                    return;
                if (position13.Tag != null)
                {
                    piece = position13.Tag as Piece;
                    if(piece.color == players[currentPlayerIndex].name)
                    {
                        return;
                    }
                    PictureBox home = checkHome(piece.color);
                    home.Tag = piece;
                    piece.isAtHome = true;
                    string rp = @"Assets/" + piece.color + "TilePiece.png";
                    home.Image = Image.FromFile(rp);

                    piece = p.Tag as Piece;
                    position13.Tag = piece;
                    piece.isAtHome = false;
                    rp = @"Assets\bluePiece.png";
                    position13.Image = Image.FromFile(rp);


                    rp = @"Assets\blueTile.png";
                    p.Image = Image.FromFile(rp);
                    p.Tag = null;
                    moved = false;
                    diceRolled = false;
                }
                else
                {
                    piece = p.Tag as Piece;
                    position13.Tag = piece;
                    piece.isAtHome = false;
                    string rp = @"Assets\bluePiece.png";
                    position13.Image = Image.FromFile(rp);

                    rp = @"Assets\blueTile.png";
                    p.Image = Image.FromFile(rp);
                    p.Tag = null;
                    moved = false;
                    diceRolled = false;
                }
            }
        }
        private void redHome_Click(object sender, EventArgs e)
        {
            if (!diceRolled)
            {
                MessageBox.Show("Roll the dice first!");
                return;

            }
            else if (diceValue != 6 || moved)
            {
                return;
            }
            PictureBox p = (PictureBox)sender;
            if (p.Tag != null)
            {
                Piece piece = p.Tag as Piece;
                if (players[currentPlayerIndex].name != piece.color)
                    return;
                if (position25.Tag != null)
                {
                    piece = position25.Tag as Piece;
                    if (players[currentPlayerIndex].name == piece.color)
                    {
                        return;
                    }
                    PictureBox home = checkHome(piece.color);
                    home.Tag = piece;
                    piece.isAtHome = true;
                    string rp = @"Assets/" + piece.color + "TilePiece.png";
                    home.Image = Image.FromFile(rp);

                    piece = p.Tag as Piece;
                    position25.Tag = piece;
                    piece.isAtHome = false;
                    rp = @"Assets\redPiece.png";
                    position25.Image = Image.FromFile(rp);

                    rp = @"Assets\redTile.png";
                    p.Image = Image.FromFile(rp);
                    p.Tag = null;
                    moved = false;
                    diceRolled = false;
                }
                else
                {
                    piece = p.Tag as Piece;
                    position25.Tag = piece;
                    piece.isAtHome = false;
                    string rp = @"Assets\redPiece.png";
                    position25.Image = Image.FromFile(rp);

                    rp = @"Assets\redTile.png";
                    p.Image = Image.FromFile(rp);
                    p.Tag = null;
                    moved = false;
                    diceRolled = false;
                }
            }
        }
        private void greenHome_Click(object sender, EventArgs e)
        {
            if (!diceRolled)
            {
                MessageBox.Show("Roll the dice first!");
                return;

            }
            else if (diceValue != 6 || moved)
            {
                return;
            }
            PictureBox p = (PictureBox)sender;
            if (p.Tag != null)
            {
                Piece piece = p.Tag as Piece;
                if (players[currentPlayerIndex].name != piece.color)
                    return;
                if (position37.Tag != null)
                {
                    piece = position37.Tag as Piece;
                    if (piece.color == players[currentPlayerIndex].name)
                    {
                        return;
                    }
                    PictureBox home = checkHome(piece.color);
                    home.Tag = piece;
                    piece.isAtHome = true;
                    string rp = @"Assets/" + piece.color + "TilePiece.png";
                    home.Image = Image.FromFile(rp);

                    piece = p.Tag as Piece;
                    position37.Tag = piece;
                    piece.isAtHome = false;
                    rp = @"Assets\greenPiece.png";
                    position37.Image = Image.FromFile(rp);

                    rp = @"Assets\greenTile.png";
                    p.Image = Image.FromFile(rp);
                    p.Tag = null;
                    moved = false;
                    diceRolled = false;
                }
                else
                {
                    piece = p.Tag as Piece;
                    position37.Tag = piece;
                    piece.isAtHome = false;
                    string rp = @"Assets\greenPiece.png";
                    position37.Image = Image.FromFile(rp);

                    rp = @"Assets\greenTile.png";
                    p.Image = Image.FromFile(rp);
                    p.Tag = null;
                    moved = false;
                    diceRolled = false;
                }
            }
        }




        public void updateUI()
        {
            Player player = players[currentPlayerIndex];
            if (player.name == "yellow")
            {
                yellowBar.Value = 100;
                blueBar.Value = 0;
                greenBar.Value = 0;
                redBar.Value = 0;
            }
            else if (player.name == "blue")
            {
                blueBar.Value = 100;
                yellowBar.Value = 0;
                greenBar.Value = 0;
                redBar.Value = 0;
            }
            else if (player.name == "red")
            {
                blueBar.Value = 0;
                yellowBar.Value = 0;
                greenBar.Value = 0;
                redBar.Value = 100;
            }
            else if (player.name == "green")
            {
                blueBar.Value = 0;
                yellowBar.Value = 0;
                greenBar.Value = 100;
                redBar.Value = 0;
            }
           

        }

        private void endTurnBtn_Click(object sender, EventArgs e)
        {
             Player currentPlayer = players[currentPlayerIndex];

             bool allPiecesAtHome = currentPlayer.pieces.All(piece => piece.isAtHome);
             bool pieceAtEnd = currentPlayer.pieces.Any(piece => piece.isFinished);


             if ((allPiecesAtHome && diceValue != 6) || (pieceAtEnd))
             {
                 moved = true;
             }
        
            if (!moved)
            {
                MessageBox.Show("Play your turn first!");
                return;
            }
            moved = false;
            diceValue = 0;
            diceRolled = false;
            currentPlayerIndex++;
            if (currentPlayerIndex == numOfPlayers)
            {
                currentPlayerIndex = 0;
            }
            AttachClickEvents();
            updateUI();
        }
        public PictureBox checkHome(string color)
        {
            List<PictureBox> homes = new List<PictureBox>();
            foreach (PictureBox h in allHomes)
            {
                if (h.Name.StartsWith(color))
                {
                    homes.Add(h);
                }
            }
            foreach (PictureBox home in homes)
            {
                if (home.Tag == null)
                {
                    return home;
                }
            }
            return null;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkWinner(new List<PictureBox> { positionY1, positionY2, positionY3, positionY4 }, "Yellow");
            checkWinner(new List<PictureBox> { positionB1, positionB2, positionB3, positionB4 }, "Blue");
            checkWinner(new List<PictureBox> { positionG1, positionG2, positionG3, positionG4 }, "Green");
            checkWinner(new List<PictureBox> { positionR1, positionR2, positionR3, positionR4 }, "Red");
        }

        private void checkWinner(List<PictureBox> homes, string playerName)
        {
            int counter = 0;
            foreach (PictureBox home in homes)
            {
                if (home.Tag != null)
                {
                    counter++;
                }
            }
            if (counter == 4)
            {
                timer1.Stop();
                MessageBox.Show($"{playerName} player has won the game,\n Congratulations!!!");
                this.Close();
            }
        }
    }
}
