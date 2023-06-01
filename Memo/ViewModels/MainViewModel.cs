using Memo.Infrastructure.Constants;
using Memo.Models;
using Memo.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace Memo.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        public bool StepFirst = true;
        public Field SelectedField = null;
        
        private ObservableCollection<ObservableCollection<Field>> a = new();
        public ObservableCollection<ObservableCollection<Field>> A { get => a; set => Set(ref a, value); }
     
        public MainViewModel()
        {
            StartGame();
        }
        private int pointPlayer1 = 0;
        public int PointPlayer1 { get => pointPlayer1; set => Set(ref pointPlayer1, value); }


        private int pointPlayer2 = 0;
        public int PointPlayer2 { get => pointPlayer2; set => Set(ref pointPlayer2, value); }


        readonly Random rnd = new();


        public void StartGame()
        {
            StepFirst = true;
            GenerateField();
            GenerateImage();
            PointPlayer1 = 0;
            PointPlayer2 = 0;
        }


        public void RestartGame()
        {
            MessageBoxResult results = MessageBox.Show("Начать заново", "Конец игры", MessageBoxButton.YesNo);
            if (results == MessageBoxResult.Yes)
            {
                StartGame();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        public void GenerateField()
        {
            a.Clear();
            for (int i = 0; i < 5; i++)
            {
                ObservableCollection<Field> row = new();
                for (int j = 0; j < 6; j++)
                {
                    row.Add(new Field() { I = i, J = j });
                }
                a.Add(row);
            }
        }
        
        public void GenerateImage()
        {
            for (int i = 0; i < ImageArray.images.Length; i++)
            {
                int added = 0;
                for (int j = 0; j < 2 + added; j++)
                {
                    int w = rnd.Next(0, 5);
                    int h = rnd.Next(0, 6);
                    if (A[w][h].image != ImageArray.Black)
                    {
                        added++;
                        continue;
                    }
                    A[w][h].image = ImageArray.images[i];
                }
            }
        }

        public bool FindOpenedField()
        {
            List<Field> opened = new();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (A[i][j].image == A[i][j].ImagePath && A[i][j].Enable)
                    {
                        opened.Add(A[i][j]);
                    }
                }
            }
            if (opened.Count == 2)
            {
                opened[0].ImagePath = ImageArray.Black;
                opened[1].ImagePath = ImageArray.Black;
                return true;
            }
            return false;
        }


        public void ClickField(Field field)
        {
            if (FindOpenedField()) return;
            if (SelectedField == null)
            {
                SelectedField = field;
                SelectedField.ImagePath = SelectedField.image;
            }
            else
            {
                if (field == SelectedField)
                {
                    return;
                }
                if (SelectedField.ImagePath == field.image)
                {
                    field.ImagePath = field.image;
                    SelectedField.Enable = false;
                    field.Enable = false;
                    if (StepFirst)
                    {
                        PointPlayer1 += 1;
                    }
                    else
                    {
                        PointPlayer2 += 1;
                    }

                }
                else
                {
                    field.ImagePath = field.image;
                    StepFirst = !StepFirst;
                }
                SelectedField = null;
                CheckPoint();
            }
        }

        public void CheckPoint()
        {
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (!A[i][j].Enable)
                    {
                        count++;
                    }
                }
            }
            if (count == 30)
            {
                if (pointPlayer1 > pointPlayer2)
                {
                    MessageBox.Show("Игрок 1 победил!");
                    RestartGame();

                }
                else if (pointPlayer1 < pointPlayer2)
                {
                    MessageBox.Show("Игрок 2 победил!");
                    RestartGame();
                }
            
            }
        }

    }
}
