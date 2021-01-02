﻿using System;
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

namespace LORModingBase.UC
{
    /// <summary>
    /// EditCard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EditCard : UserControl
    {
        DS.CardInfo innerCardInfo = null;
        Action initStack = null;

        #region Init controls
        public EditCard(DS.CardInfo innerCardInfo, Action initStack)
        {
            this.innerCardInfo = innerCardInfo;
            this.initStack = initStack;
            InitializeComponent();

            ChangeRarityUIInit(innerCardInfo.rarity);
            TbxCardName.Text = innerCardInfo.name;
            TbxCardUniqueID.Text = innerCardInfo.cardID;
        }

        private void ChangeRarityUIInit(string rarity)
        {
            BtnRarityCommon.Background = null;
            BtnRarityUncommon.Background = null;
            BtnRarityRare.Background = null;
            BtnRarityUnique.Background = null;

            switch (rarity)
            {
                case "Common":
                    WindowBg.Fill = Tools.ColorTools.GetSolidColorBrushByHexStr("#5430BF4B");
                    BtnRarityCommon.Background = Tools.ColorTools.GetSolidColorBrushByHexStr("#54FFFFFF");
                    innerCardInfo.rarity = "Common";
                    break;
                case "Uncommon":
                    WindowBg.Fill = Tools.ColorTools.GetSolidColorBrushByHexStr("#54306ABF");
                    BtnRarityUncommon.Background = Tools.ColorTools.GetSolidColorBrushByHexStr("#54FFFFFF");
                    innerCardInfo.rarity = "Uncommon";
                    break;
                case "Rare":
                    WindowBg.Fill = Tools.ColorTools.GetSolidColorBrushByHexStr("#548030BF");
                    BtnRarityRare.Background = Tools.ColorTools.GetSolidColorBrushByHexStr("#54FFFFFF");
                    innerCardInfo.rarity = "Rare";
                    break;
                case "Unique":
                    WindowBg.Fill = Tools.ColorTools.GetSolidColorBrushByHexStr("#54F3B530");
                    BtnRarityUnique.Background = Tools.ColorTools.GetSolidColorBrushByHexStr("#54FFFFFF");
                    innerCardInfo.rarity = "Unique";
                    break;
            }
        } 
        #endregion

        #region Rarity buttons
        private void BtnRarityCommon_Click(object sender, RoutedEventArgs e)
        {
            ChangeRarityUIInit("Common");
        }

        private void BtnRarityUncommon_Click(object sender, RoutedEventArgs e)
        {
            ChangeRarityUIInit("Uncommon");
        }

        private void BtnRarityRare_Click(object sender, RoutedEventArgs e)
        {
            ChangeRarityUIInit("Rare");
        }

        private void BtnRarityUnique_Click(object sender, RoutedEventArgs e)
        {
            ChangeRarityUIInit("Unique");
        }
        #endregion
        #region Right side buttons
        private void BtnExtraInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDropCards_Click(object sender, RoutedEventArgs e)
        {

        }


        private void BtnRangeType_Click(object sender, RoutedEventArgs e)
        {

        }


        private void BtnCopyCard_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.cardInfos.Add(Tools.DeepCopy.DeepClone(innerCardInfo));
            initStack();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.cardInfos.Remove(innerCardInfo);
            initStack();
        }
        #endregion
        #region Left side buttons
        private void BtnCardImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCardEffect_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Text change events
        private void TbxCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            innerCardInfo.cost = TbxCost.Text;
        }

        private void TbxCardName_TextChanged(object sender, TextChangedEventArgs e)
        {
            innerCardInfo.name = TbxCardName.Text;
        }

        private void TbxCardUniqueID_TextChanged(object sender, TextChangedEventArgs e)
        {
            innerCardInfo.cardID = TbxCardUniqueID.Text;
        }
        #endregion

        #region Controls for dices
        private void BtnAddDice_Click(object sender, RoutedEventArgs e)
        {
            SqlDices.Children.Add(new EditDice());
        }
        #endregion
    }
}
