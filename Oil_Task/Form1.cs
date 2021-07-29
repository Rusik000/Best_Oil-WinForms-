using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Oil_Task
{
    public partial class Form1 : Form
    {

        private readonly Department _department = new Department();
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Blue;
            PetrolStationLbl.ForeColor = Color.Chocolate;
            MiniCafeLbl.ForeColor = Color.Red;


            InitializePetrolStationWithGasolines();
            GasolineTypesCmbBx.DataSource = _department.PetrolStation.Gasolines;
            GasolineTypesCmbBx.DisplayMember = "Name";

            InitializeMiniCafeWithMeals();

        }


        private void InitializePetrolStationWithGasolines()
        {
            _department.AddGasoline
             (
                new Gasoline
                {
                    Name = "BP",
                    Price = 9.9
                }
             );


            _department.AddGasoline
             (
                new Gasoline
                {
                    Name = "Speedway",
                    Price = 5.3

                }
             );

            _department.AddGasoline
           (
               new Gasoline
               {
                   Name = "Delta",
                   Price = 2.1
               }
           );


            _department.AddGasoline
             (
                new Gasoline
                {
                    Name = "Bates Oil",
                    Price = 4.8
                }
             );

            _department.AddGasoline
             (
                new Gasoline
                {
                    Name = "Cosmo Oil",
                    Price = 3.2
                }
             );
        }
        private void InitializeMiniCafeWithMeals()
        {
            _department.AddMeal
             (
                new Meal
                {
                    Name = "Big Mac",
                    Price = 5.0,
                    Count = 0
                }
             );

            _department.AddMeal
             (
                new Meal
                {
                    Name = "World Famous Fries",
                    Price = 4.2,
                    Count = 0
                }
             );

            _department.AddMeal
             (
                new Meal
                {
                    Name = "Burrito",
                    Price = 3.9,
                    Count = 0
                }
             );

            _department.AddMeal
             (
                new Meal
                {
                    Name = "Iced Coffee",
                    Price = 3.4,
                    Count = 0
                }
             );




        }





       

        private void SetBPTotalPrice()
        {
            string result = (double.Parse(TotalPriceLbl.Text) + double.Parse(MiniCafeTotalPriceLbl.Text)).ToString();
            BPTotalPayLbl.Text = result;
        }


        private void GasolineTypesCmbBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            PricePrinterLbl.Text = (GasolineTypesCmbBx.SelectedItem as Gasoline)?.Price.ToString();

            if (LitrRdBtn.Checked)
            {
                if (double.TryParse(LitrInputMskdTxtBx.Text, out double number))
                {
                    string result = (number * (GasolineTypesCmbBx.SelectedItem as Gasoline)?.Price).ToString();
                    TotalPriceLbl.Text = result;
                    SetBPTotalPrice();
                }
            }

            else if (MoneyRdBtn.Checked)
            {
                if (double.TryParse(MoneyInputMskdTxtBx.Text, out _))
                {
                    TotalPriceLbl.Text = MoneyInputMskdTxtBx.Text;
                    SetBPTotalPrice();
                }
            }

        }

        private void LitrRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (LitrRdBtn.Checked)
            {
                LitrInputMskdTxtBx.Enabled = true;
                MoneyInputMskdTxtBx.Enabled = false;
                LitrInputMskdTxtBx.BackColor = Color.White;
                MoneyInputMskdTxtBx.Text = default;
            }
            else
            {
                LitrInputMskdTxtBx.Enabled = false;
                LitrInputMskdTxtBx.BackColor = SystemColors.ScrollBar;
                TotalPriceLbl.Text = "0";
                SetBPTotalPrice();
            }

        }

        private void MoneyRdBt_CheckedChanged(object sender, EventArgs e)
        {
            if (MoneyRdBtn.Checked)
            {
                MoneyInputMskdTxtBx.Enabled = true;
                LitrInputMskdTxtBx.Enabled = false;
                MoneyInputMskdTxtBx.BackColor = Color.White;
                LitrInputMskdTxtBx.Text = default;
            }
            else
            {
                MoneyInputMskdTxtBx.Enabled = false;
                MoneyInputMskdTxtBx.BackColor = SystemColors.ScrollBar;
                TotalPriceLbl.Text = "0";
                SetBPTotalPrice();
            }
        }

        private void LitrInputMskdTxtBx_KeyUp(object sender, KeyEventArgs e)
        {
            if (double.TryParse(LitrInputMskdTxtBx.Text, out double number))
            {
                string result = (number * (GasolineTypesCmbBx.SelectedItem as Gasoline)?.Price).ToString();
                TotalPriceLbl.Text = result;
            }
            else
                TotalPriceLbl.Text = "0";

            SetBPTotalPrice();
        }

        private void MoneyInputMskdTxtBx_KeyUp(object sender, KeyEventArgs e)
        {
            if (double.TryParse(MoneyInputMskdTxtBx.Text, out _))
            {
                TotalPriceLbl.Text = MoneyInputMskdTxtBx.Text;
            }
            else
                TotalPriceLbl.Text = "0";

            SetBPTotalPrice();
        }

        private int FindIndexWithCheckedeBoxName(CheckBox checkBox)
        {
            if (checkBox.Text == MealNames.BigMac)
                return MealNamesIndexes.BigMac;

            if (checkBox.Text == MealNames.WorldFamousFries)
                return MealNamesIndexes.WorldFamousFries;

            if (checkBox.Text == MealNames.Burrito)
                return MealNamesIndexes.Burrito;

            if (checkBox.Text == MealNames.IcedCoffe)
                return MealNamesIndexes.IcedCoffee;

            throw new InvalidOperationException($"There is no {checkBox.Text} exists in the meals.");
        }
        private void DoOperationsWithCheckBoxAndMaskedTextBox(CheckBox checkBox, MaskedTextBox maskedTextBox)
        {
            if (checkBox.Checked)
            {
                maskedTextBox.Enabled = true;
                maskedTextBox.BackColor = Color.White;
            }
            else
            {
                if (double.TryParse(maskedTextBox.Text, out double number))
                {

                    double result = double.Parse(MiniCafeTotalPriceLbl.Text) -
                        number * _department.MiniCafe.Meals[FindIndexWithCheckedeBoxName(checkBox)].Price;
                    MiniCafeTotalPriceLbl.Text = result.ToString();
                    SetBPTotalPrice();
                }

                maskedTextBox.BackColor = SystemColors.ScrollBar;
                maskedTextBox.Text = default;
                maskedTextBox.Enabled = false;
            }
        }
        private void BigMacChkBx_CheckedChanged(object sender, EventArgs e)
        {
            DoOperationsWithCheckBoxAndMaskedTextBox(BigMacChkBx, HowMuchMskdTxtBx1);
        }

        private void FriesChkBx_CheckedChanged(object sender, EventArgs e)
        {
            DoOperationsWithCheckBoxAndMaskedTextBox(FriesChkBx, HowMuchMskdTxtBx2);
        }

        private void BurritoChkBx_CheckedChanged(object sender, EventArgs e)
        {
            DoOperationsWithCheckBoxAndMaskedTextBox(BurritoChkBx, HowMuchMskdTxtBx3);
        }

        private void IcedCoffeChkBx_CheckedChanged(object sender, EventArgs e)
        {
            DoOperationsWithCheckBoxAndMaskedTextBox(IcedCoffeeChkBx, HowMuchMskdTxtBx4);
        }

        private void DoOperationWhenKeyUp()
        {
            double result = 0.0;
            if (double.TryParse(HowMuchMskdTxtBx1.Text, out double number1))
            {
                result += number1 * _department.MiniCafe.Meals[MealNamesIndexes.BigMac].Price;
            }

            if (double.TryParse(HowMuchMskdTxtBx2.Text, out double number2))
            {
                result += number2 * _department.MiniCafe.Meals[MealNamesIndexes.WorldFamousFries].Price;
            }

            if (double.TryParse(HowMuchMskdTxtBx3.Text, out double number3))
            {
                result += number3 * _department.MiniCafe.Meals[MealNamesIndexes.Burrito].Price;
            }

            if (double.TryParse(HowMuchMskdTxtBx4.Text, out double number4))
            {
                result += number4 * _department.MiniCafe.Meals[MealNamesIndexes.IcedCoffee].Price;
            }

            MiniCafeTotalPriceLbl.Text = result.ToString();
            SetBPTotalPrice();
        }
        private void HowMuchMskdTxtBx1_KeyUp(object sender, KeyEventArgs e)
        {
            DoOperationWhenKeyUp();
        }

        private void HowMuchMskdTxtBx2_KeyUp(object sender, KeyEventArgs e)
        {
            DoOperationWhenKeyUp();
        }

        private void HowMuchMskdTxtBx3_KeyUp(object sender, KeyEventArgs e)
        {
            DoOperationWhenKeyUp();
        }

        private void HowMuchMskdTxtBx4_KeyUp(object sender, KeyEventArgs e)
        {
            DoOperationWhenKeyUp();
        }

        private void PayBtn_Click(object sender, EventArgs e)
        {
            #region JsonFile

            //if (LitrRdBtn.Checked)
            //{
            //    _department.PetrolStation.Gasolines[GasolineTypesCmbBx.SelectedIndex].
            //      DescriptionAboutBoughtGasoline = $"Bought {LitrInputMskdTxtBx.Text} litres and pay {TotalPriceLbl.Text} $";

            //    _department.PetrolStation.BoughtLitr = int.Parse(LitrInputMskdTxtBx.Text);
            //    _department.PetrolStation.BoughtPrice = 0;

            //}
            //else if (MoneyRdBtn.Checked)
            //{
            //    _department.PetrolStation.Gasolines[GasolineTypesCmbBx.SelectedIndex].
            //        DescriptionAboutBoughtGasoline = $"Bought with money and pay {TotalPriceLbl.Text} $";

            //    _department.PetrolStation.BoughtLitr = 0;
            //    _department.PetrolStation.BoughtPrice = int.Parse(MoneyInputMskdTxtBx.Text);

            //}
            //_department.PetrolStation.AllPrice = double.Parse(TotalPriceLbl.Text);


            //if (BigMacChkBx.Checked)
            //    _department.MiniCafe.Meals[MealNamesIndexes.BigMac].Count = int.Parse(HowMuchMskdTxtBx1.Text);
            //if (FriesChkBx.Checked)
            //    _department.MiniCafe.Meals[MealNamesIndexes.WorldFamousFries].Count = int.Parse(HowMuchMskdTxtBx2.Text);
            //if (BurritoChkBx.Checked)
            //    _department.MiniCafe.Meals[MealNamesIndexes.Burrito].Count = int.Parse(HowMuchMskdTxtBx3.Text);
            //if (IcedCoffeeChkBx.Checked)
            //    _department.MiniCafe.Meals[MealNamesIndexes.IcedCoffee].Count = int.Parse(HowMuchMskdTxtBx4.Text);

            //_department.MiniCafe.AllCount = _department.MiniCafe.Meals[MealNamesIndexes.BigMac].Count +
            //                                _department.MiniCafe.Meals[MealNamesIndexes.WorldFamousFries].Count +
            //                                _department.MiniCafe.Meals[MealNamesIndexes.Burrito].Count +
            //                                _department.MiniCafe.Meals[MealNamesIndexes.IcedCoffee].Count;

            //_department.MiniCafe.AllPrice = double.Parse(MiniCafeTotalPriceLbl.Text);
            //_department.AllPetrolAndMiniCafePaidPrice = double.Parse(BPTotalPayLbl.Text);
            //JsonFileHelper.JSONSerialization(_department);

            #endregion

            if (BPTotalPayLbl.Text != "0")
            {
                if (!Directory.Exists("DataBase"))
                    Directory.CreateDirectory("DataBase");

                WriteDepartmentToPdfFile();
            }

            MessageBox.Show("Good Day", "Department of BP");
            ReturnDefaultValues();
        }


        private void ReturnDefaultValues()
        {
            GasolineTypesCmbBx.SelectedIndex = 0;
            LitrRdBtn.Checked = false;
            LitrInputMskdTxtBx.Text = default;

            MoneyRdBtn.Checked = false;
            MoneyInputMskdTxtBx.Text = default;

            BigMacChkBx.Checked = false;
            HowMuchMskdTxtBx1.Text = default;

            FriesChkBx.Checked = false;
            HowMuchMskdTxtBx2.Text = default;

            BurritoChkBx.Checked = false;
            HowMuchMskdTxtBx3.Text = default;

            IcedCoffeeChkBx.Checked = false;
            HowMuchMskdTxtBx4.Text = default;

            TotalPriceLbl.Text = "0";
            MiniCafeTotalPriceLbl.Text = "0";
            BPTotalPayLbl.Text = "0";

        }


        private void WriteDepartmentToPdfFile()
        {
            var spacer = new Paragraph("")
            {
                SpacingBefore = 10f,
                SpacingAfter = 10f,
            };



            var document = new Document(PageSize.LETTER, 40f, 40f, 60f, 60f);
            PdfWriter pw = PdfWriter.GetInstance(document, new FileStream($"DataBase/{Guid.NewGuid()}.pdf", FileMode.OpenOrCreate));


            document.Open();
            


            document.Add
            (
                new Paragraph("")
                {
                    SpacingBefore = 70f,
                }
            );


            if (int.TryParse(LitrInputMskdTxtBx.Text, out int litrNumber) || int.TryParse(MoneyInputMskdTxtBx.Text, out int moneyNumber)
                 && (litrNumber > 0 || moneyNumber > 0))
            {
                if (LitrRdBtn.Checked)
                {
                    var pdfTable = new PdfPTable(3)
                    {
                        HorizontalAlignment = Left,
                        WidthPercentage = 100,
                        DefaultCell = { MinimumHeight = 22f }
                    };

                    pdfTable.AddCell(PdfHelper.CreateNewCell("Petrol Station", 3));


                    pdfTable.AddCell(PdfHelper.CreateNewCell("Name", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell("Price", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell("Bought Litres", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{_department.PetrolStation.Gasolines[GasolineTypesCmbBx.SelectedIndex].Name}", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{_department.PetrolStation.Gasolines[GasolineTypesCmbBx.SelectedIndex].Price} $", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{LitrInputMskdTxtBx.Text} litres", 1));


                    var cellEnd = new PdfPCell(new Phrase($"Total Price {TotalPriceLbl.Text} $"))
                    {
                        Colspan = 3,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        PaddingTop = 10,
                        PaddingBottom = 10
                    };
                    pdfTable.AddCell(cellEnd);

                    document.Add(pdfTable);
                }
                else
                {
                    var pdfTable = new PdfPTable(2)
                    {
                        HorizontalAlignment = Left,
                        WidthPercentage = 100,
                        DefaultCell = { MinimumHeight = 22f }
                    };

                    pdfTable.AddCell(PdfHelper.CreateNewCell("Petrol Station", 2));


                    pdfTable.AddCell(PdfHelper.CreateNewCell("Name", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell("Price", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{_department.PetrolStation.Gasolines[GasolineTypesCmbBx.SelectedIndex].Name}", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{_department.PetrolStation.Gasolines[GasolineTypesCmbBx.SelectedIndex].Price:C} $", 1));

                    var cellEnd = new PdfPCell(new Phrase($"Total Price {TotalPriceLbl.Text} $"))
                    {
                        Colspan = 2,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        PaddingTop = 10,
                        PaddingBottom = 10
                    };
                    pdfTable.AddCell(cellEnd);

                    document.Add(pdfTable);
                }
            }



            bool bigMacFlag = int.TryParse(HowMuchMskdTxtBx1.Text, out int bigMacNumber);
            bool friesFlag = int.TryParse(HowMuchMskdTxtBx2.Text, out int friesNumber);
            bool burritoFlag = int.TryParse(HowMuchMskdTxtBx3.Text, out int burritoNumber);
            bool icedCoffeeFlag = int.TryParse(HowMuchMskdTxtBx4.Text, out int icedCoffeeNumber);




            if (bigMacFlag || friesFlag || burritoFlag || icedCoffeeFlag)
            {

                document.Add
                (
                    new Paragraph("")
                    {
                        SpacingBefore = 70f,
                    }
                );

                var pdfTable = new PdfPTable(3)
                {
                    HorizontalAlignment = Left,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 22f }
                };

                pdfTable.AddCell(PdfHelper.CreateNewCell("Mini-Cafe", 3));

                pdfTable.AddCell(PdfHelper.CreateNewCell("Name", 1));
                pdfTable.AddCell(PdfHelper.CreateNewCell("Price", 1));
                pdfTable.AddCell(PdfHelper.CreateNewCell("Count", 1));

                if (bigMacNumber != 0)
                {
                    pdfTable.AddCell(PdfHelper.CreateNewCell("Big Mac", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{_department.MiniCafe.Meals[MealNamesIndexes.BigMac].Price} $", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{HowMuchMskdTxtBx1.Text}", 1));
                }
                if (friesNumber != 0)
                {
                    pdfTable.AddCell(PdfHelper.CreateNewCell("World Famous Fries", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{_department.MiniCafe.Meals[MealNamesIndexes.WorldFamousFries].Price} $", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{HowMuchMskdTxtBx2.Text}", 1));
                }
                if (burritoNumber != 0)
                {
                    pdfTable.AddCell(PdfHelper.CreateNewCell("Burrito", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{_department.MiniCafe.Meals[MealNamesIndexes.Burrito].Price} $", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{HowMuchMskdTxtBx3.Text}", 1));
                }
                if (icedCoffeeNumber != 0)
                {
                    pdfTable.AddCell(PdfHelper.CreateNewCell("Iced Coffee", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{_department.MiniCafe.Meals[MealNamesIndexes.IcedCoffee].Price} $", 1));
                    pdfTable.AddCell(PdfHelper.CreateNewCell($"{HowMuchMskdTxtBx4.Text}", 1));
                }

                var cell = new PdfPCell(new Phrase($"Total Price {MiniCafeTotalPriceLbl.Text} $"))
                {
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    PaddingTop = 10,
                    PaddingBottom = 10
                };

                pdfTable.AddCell(cell);
                document.Add(pdfTable);

            }

            document.Add
              (
                  new Paragraph("")
                  {
                      SpacingBefore = 20f,
                  }
              );

            var paragraph = new Paragraph($"Total Paid Money Mini-Cafe and Petrol Station is {BPTotalPayLbl.Text} $");
            paragraph.Alignment = Element.ALIGN_CENTER;
            document.Add(paragraph);
            document.Close();
        }



    }
}

