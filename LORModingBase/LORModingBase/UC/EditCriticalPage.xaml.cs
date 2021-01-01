﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LORModingBase.UC
{
    /// <summary>
    /// EditCriticalPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EditCriticalPage : UserControl
    {
        DS.CriticalPageInfo innerCriticalPageInfo = null;
        Action initStack = null;

        #region Init controls
        public EditCriticalPage(DS.CriticalPageInfo criticalPageInfo, Action initStack)
        {
            InitializeComponent();
            this.innerCriticalPageInfo = criticalPageInfo;
            this.initStack = initStack;

            #region 일반적인 핵심책장 정보 UI 반영시키기
            ChangeRarityUIInit(criticalPageInfo.rarity);
            if (!string.IsNullOrEmpty(criticalPageInfo.episodeDes))
            {
                BtnEpisode.Content = criticalPageInfo.episodeDes;
                BtnEpisode.ToolTip = criticalPageInfo.episodeDes;
            }

            TbxPageName.Text = criticalPageInfo.name;
            TbxPageUniqueID.Text = criticalPageInfo.bookID;

            TbxHP.Text = criticalPageInfo.HP;
            TbxBR.Text = criticalPageInfo.breakNum;
            TbxSpeedDiceMin.Text = criticalPageInfo.minSpeedCount;
            TbxSpeedDiceMax.Text = criticalPageInfo.maxSpeedCount;

            if (!string.IsNullOrEmpty(criticalPageInfo.iconDes))
            {
                BtnBookIcon.Content = criticalPageInfo.iconDes;
                BtnBookIcon.ToolTip = criticalPageInfo.iconDes;
            }
            if (!string.IsNullOrEmpty(criticalPageInfo.iconDes))
            {
                BtnSkin.Content = criticalPageInfo.skinDes;
                BtnSkin.ToolTip = criticalPageInfo.skinDes;
            }

            BtnSResist.Content = DS.GameInfo.resistInfo_Dic[criticalPageInfo.SResist];
            BtnPResist.Content = DS.GameInfo.resistInfo_Dic[criticalPageInfo.PResist];
            BtnHResist.Content = DS.GameInfo.resistInfo_Dic[criticalPageInfo.HResist];

            BtnBSResist.Content = DS.GameInfo.resistInfo_Dic[criticalPageInfo.BSResist];
            BtnBPResist.Content = DS.GameInfo.resistInfo_Dic[criticalPageInfo.BPResist];
            BtnBHResist.Content = DS.GameInfo.resistInfo_Dic[criticalPageInfo.BHResist];

            InitLbxPassives();
            #endregion

            #region 핵심책장 설명부분 UI 반영시키기
            if (criticalPageInfo.description != "입력된 정보가 없습니다")
            {
                BtnCiricalBookInfo.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/IconYesbookInfo.png");
                BtnCiricalBookInfo.ToolTip = "핵심 책장에 대한 설명을 입력합니다 (입력됨)";
            }
            #endregion
            #region 핵심책장 드랍 책 부분 UI 반영시키기
            if (innerCriticalPageInfo.dropBooks.Count > 0)
            {
                BtnDropBooks.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/iconYesDropBook.png");
                BtnDropBooks.ToolTip = "이 핵심책장이 어느 책에서 드랍되는지 입력합니다 (입력됨)";
            }
            #endregion

            #region 핵심책장 원거리 속성 UI 반영시키기
            if(criticalPageInfo.rangeType == "Range")
            {
                BtnRangeType.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/TypeRange.png");
                BtnRangeType.ToolTip = "클릭시 원거리 속성을 변경합니다. (현재 : 원거리 전용 책장)";
            }
            else if (criticalPageInfo.rangeType == "Hybrid")
            {
                BtnRangeType.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/TypeHybrid.png");
                BtnRangeType.ToolTip = "클릭시 원거리 속성을 변경합니다. (현재 : 하이브리드 책장)";
            }
            else
            {
                BtnRangeType.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/TypeNomal.png");
                BtnRangeType.ToolTip = "클릭시 원거리 속성을 변경합니다. (현재 : 일반 책장)";
            }
            #endregion
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
                    innerCriticalPageInfo.rarity = "Common";
                    break;
                case "Uncommon":
                    WindowBg.Fill = Tools.ColorTools.GetSolidColorBrushByHexStr("#54306ABF");
                    BtnRarityUncommon.Background = Tools.ColorTools.GetSolidColorBrushByHexStr("#54FFFFFF");
                    innerCriticalPageInfo.rarity = "Uncommon";
                    break;
                case "Rare":
                    WindowBg.Fill = Tools.ColorTools.GetSolidColorBrushByHexStr("#548030BF");
                    BtnRarityRare.Background = Tools.ColorTools.GetSolidColorBrushByHexStr("#54FFFFFF");
                    innerCriticalPageInfo.rarity = "Rare";
                    break;
                case "Unique":
                    WindowBg.Fill = Tools.ColorTools.GetSolidColorBrushByHexStr("#54F3B530");
                    BtnRarityUnique.Background = Tools.ColorTools.GetSolidColorBrushByHexStr("#54FFFFFF");
                    innerCriticalPageInfo.rarity = "Unique";
                    break;
            }
        }
        
        private void InitResistFromButton(Button targetButton, bool isLeft)
        {
            // Down index
            if(isLeft)
            {
                int RESISTS_INDEX = DS.GameInfo.resistInfo_Doc.IndexOf(targetButton.Content.ToString()) - 1;
                if (RESISTS_INDEX < 0) RESISTS_INDEX = DS.GameInfo.resistInfo_Doc.Count - 1;
                targetButton.Content = DS.GameInfo.resistInfo_Doc[RESISTS_INDEX];
            }
            // Up index
            else
            {
                int RESISTS_INDEX = DS.GameInfo.resistInfo_Doc.IndexOf(targetButton.Content.ToString()) + 1;
                if (RESISTS_INDEX >= DS.GameInfo.resistInfo_Doc.Count) RESISTS_INDEX = 0;
                targetButton.Content = DS.GameInfo.resistInfo_Doc[RESISTS_INDEX];
            }

            switch (targetButton.Name)
            {
                case "BtnSResist":
                    innerCriticalPageInfo.SResist = DS.GameInfo.resistInfo_Dic_Rev[targetButton.Content.ToString()];
                    break;
                case "BtnPResist":
                    innerCriticalPageInfo.PResist = DS.GameInfo.resistInfo_Dic_Rev[targetButton.Content.ToString()];
                    break;
                case "BtnHResist":
                    innerCriticalPageInfo.HResist = DS.GameInfo.resistInfo_Dic_Rev[targetButton.Content.ToString()];
                    break;

                case "BtnBSResist":
                    innerCriticalPageInfo.BSResist = DS.GameInfo.resistInfo_Dic_Rev[targetButton.Content.ToString()];
                    break;
                case "BtnBPResist":
                    innerCriticalPageInfo.BPResist = DS.GameInfo.resistInfo_Dic_Rev[targetButton.Content.ToString()];
                    break;
                case "BtnBHResist":
                    innerCriticalPageInfo.BHResist = DS.GameInfo.resistInfo_Dic_Rev[targetButton.Content.ToString()];
                    break;
            }
        }
        #endregion
        #region Button events
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

        private void BtnEpisode_Click(object sender, RoutedEventArgs e)
        {
            new SubWindows.InputEpisodeWindow((string chapter, string episodeID, string episodeDoc) =>
            {
                string CONTENT_TO_SHOW = $"{DS.GameInfo.chapter_Dic[chapter]} / {episodeDoc}:{episodeID}";
                BtnEpisode.Content = CONTENT_TO_SHOW;
                BtnEpisode.ToolTip = CONTENT_TO_SHOW;

                innerCriticalPageInfo.chapter = chapter;
                innerCriticalPageInfo.episode = episodeID;
                innerCriticalPageInfo.episodeDes = CONTENT_TO_SHOW;
            }).ShowDialog();
        }
        private void BtnBookIcon_Click(object sender, RoutedEventArgs e)
        {
            new SubWindows.InputBookIconWindow((string chpater, string bookIconName, string bookIconDesc) =>
            {
                string ICON_DESC = $"{bookIconDesc}:{bookIconName}";
                BtnBookIcon.Content = ICON_DESC;
                BtnBookIcon.ToolTip = ICON_DESC;

                innerCriticalPageInfo.iconName = bookIconName;
                innerCriticalPageInfo.iconDes = ICON_DESC;
            }).ShowDialog();
        }
        private void BtnSkin_Click(object sender, RoutedEventArgs e)
        {
            new SubWindows.InputBookSkinWindow((string chpater, string bookSkinName, string bookSkinDesc) =>
            {
                string SKIN_DESC = $"{bookSkinDesc}:{bookSkinName}";
                BtnSkin.Content = SKIN_DESC;
                BtnSkin.ToolTip = SKIN_DESC;

                innerCriticalPageInfo.skinName = bookSkinName;
                innerCriticalPageInfo.skinDes = SKIN_DESC;
            }).ShowDialog();
        }

        #region HP resist buttons
        private void BtnSResist_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnSResist, true);
        }

        private void BtnSResist_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnSResist, false);
        }

        private void BtnPResist_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnPResist, true);
        }

        private void BtnPResist_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnPResist, false);
        }

        private void BtnHResist_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnHResist, true);
        }

        private void BtnHResist_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnHResist, false);
        }
        #endregion
        #region Break resist buttons
        private void BtnBSResist_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnBSResist, true);
        }

        private void BtnBSResist_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnBSResist, false);
        }


        private void BtnBPResist_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnBPResist, true);
        }

        private void BtnBPResist_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnBPResist, false);
        }

        private void BtnBHResist_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnBHResist, true);
        }

        private void BtnBHResist_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitResistFromButton(BtnBHResist, false);
        }
        #endregion

        #endregion

        #region Passive events
        private void InitLbxPassives()
        {
            LbxPassives.Items.Clear();
            innerCriticalPageInfo.passiveIDs.ForEach((string passiveName) =>
            {
                LbxPassives.Items.Add(passiveName);
            });
        }

        private void BtnAddPassive_Click(object sender, RoutedEventArgs e)
        {
            new SubWindows.InputBookPassiveWindow((string passiveDec) =>
            {
                innerCriticalPageInfo.passiveIDs.Add(passiveDec);
                InitLbxPassives();
            }).ShowDialog();
        }

        private void BtnDeletePassive_Click(object sender, RoutedEventArgs e)
        {
            if(LbxPassives.SelectedItem != null)
            {
                int passiveIndexToDelete = innerCriticalPageInfo.passiveIDs.FindIndex((string passiveName) => {
                    return passiveName == LbxPassives.SelectedItem.ToString();
                });
                if(passiveIndexToDelete != -1)
                {
                    innerCriticalPageInfo.passiveIDs.RemoveAt(passiveIndexToDelete);
                    InitLbxPassives();
                }
            }
        }
        #endregion
        #region Text change events
        private void TbxPageName_TextChanged(object sender, TextChangedEventArgs e)
        {
            innerCriticalPageInfo.name = TbxPageName.Text;
        }

        private void TbxPageUniqueID_TextChanged(object sender, TextChangedEventArgs e)
        {
            innerCriticalPageInfo.bookID = TbxPageUniqueID.Text;
        }

        private void TbxHP_TextChanged(object sender, TextChangedEventArgs e)
        {
            innerCriticalPageInfo.HP = TbxHP.Text;
        }

        private void TbxBR_TextChanged(object sender, TextChangedEventArgs e)
        {
            innerCriticalPageInfo.breakNum = TbxBR.Text;
        }

        private void TbxSpeedDiceMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            innerCriticalPageInfo.minSpeedCount = TbxSpeedDiceMin.Text;
        }

        private void TbxSpeedDiceMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            innerCriticalPageInfo.maxSpeedCount = TbxSpeedDiceMax.Text;
        }
        #endregion

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.criticalPageInfos.Remove(innerCriticalPageInfo);
            initStack();
        }

        #region Right button events (Upside)
        private void BtnCiricalBookInfo_Click(object sender, RoutedEventArgs e)
        {
            new SubWindows.InputCriticalBookDescription((string inputedDes) =>
            {
                innerCriticalPageInfo.description = inputedDes;
                BtnCiricalBookInfo.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/IconYesbookInfo.png");
                BtnCiricalBookInfo.ToolTip = "핵심 책장에 대한 설명을 입력합니다 (입력됨)";
            }, innerCriticalPageInfo.description).ShowDialog();
        }

        private void BtnDropBooks_Click(object sender, RoutedEventArgs e)
        {
            new SubWindows.InputDropBookInfosWindow(innerCriticalPageInfo.dropBooks).ShowDialog();

            if(innerCriticalPageInfo.dropBooks.Count > 0)
            {
                BtnDropBooks.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/iconYesDropBook.png");
                BtnDropBooks.ToolTip = "이 핵심책장이 어느 책에서 드랍되는지 입력합니다 (입력됨)";
            }
            else
            {
                BtnDropBooks.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/iconNoDropBook.png");
                BtnDropBooks.ToolTip = "이 핵심책장이 어느 책에서 드랍되는지 입력합니다 (미입력)";
            }
        }

        private void BtnEnemySetting_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        private void BtnRangeType_Click(object sender, RoutedEventArgs e)
        {
            switch(innerCriticalPageInfo.rangeType)
            {
                case "Range":
                    innerCriticalPageInfo.rangeType = "Hybrid";
                    break;
                case "Hybrid":
                    innerCriticalPageInfo.rangeType = "Nomal";
                    break;
                default:
                    innerCriticalPageInfo.rangeType = "Range";
                    break;
            }

            #region 핵심책장 원거리 속성 UI 반영시키기
            if (innerCriticalPageInfo.rangeType == "Range")
            {
                BtnRangeType.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/TypeRange.png");
                BtnRangeType.ToolTip = "클릭시 원거리 속성을 변경합니다. (현재 : 원거리 전용 책장)";
            }
            else if (innerCriticalPageInfo.rangeType == "Hybrid")
            {
                BtnRangeType.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/TypeHybrid.png");
                BtnRangeType.ToolTip = "클릭시 원거리 속성을 변경합니다. (현재 : 하이브리드 책장)";
            }
            else
            {
                BtnRangeType.Background = Tools.ColorTools.GetImageBrushFromPath(this, "../Resources/TypeNomal.png");
                BtnRangeType.ToolTip = "클릭시 원거리 속성을 변경합니다. (현재 : 일반 책장)";
            }
            #endregion
        }
    }
}
