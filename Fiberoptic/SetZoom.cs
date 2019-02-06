using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FiberopticServer
{
    public partial class SetZoom : Form
    {
        DataTable dt;
        DataRow dr;
        importdll.ZoomInfo tmpZoom = new importdll.ZoomInfo();
        List<importdll.ZoomInfo> zoomInfo = new List<importdll.ZoomInfo>();
        public SetZoom()
        {
            InitializeComponent();
            initsetZoom();
        }
        public void initsetZoom()
        {
            //设置区间信息
            dt = new DataTable();
            dt.Columns.Add("区间编号");
            dt.Columns.Add("区间名称");
            dt.Columns.Add("区间起点/m");
            dt.Columns.Add("区间终点/m");
            dt.Columns.Add("阈值设置%");
            dt.Columns.Add("异常比设置%");
            dt.Columns.Add("确认比设置%");
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("aaa");
        }

    //{
    //     if (zoomInfo.Exists(x => x.ZoomNum == Convert.ToInt32(dataGridView1[0,dataGridView1.CurrentCell.RowIndex].FormattedValue)))//list中存在该项
    //        {
    //           tmpZoom=zoomInfo[zoomInfo.FindIndex(0,x => x.ZoomNum == Convert.ToInt32(dataGridView1[0,dataGridView1.CurrentCell.RowIndex].FormattedValue))];//找到该项的值
    //            switch (dataGridView1.CurrentCell.ColumnIndex)
    //            {
    //                case 0: tmpZoom.ZoomNum = Convert.ToInt16(dataGridView1.EditingControl.Text); break;
    //                case 1: tmpZoom.ZoomName = dataGridView1.EditingControl.Text; break;
    //                case 2: tmpZoom.ZoomStartLoc = Convert.ToDouble(dataGridView1.EditingControl.Text); break;
    //                case 3: tmpZoom.ZoomEndLoc = Convert.ToDouble(dataGridView1.EditingControl.Text); break;
    //                case 4: tmpZoom.Threshold = Convert.ToDouble(dataGridView1.EditingControl.Text); break;
    //                case 5: tmpZoom.ExpRate = Convert.ToDouble(dataGridView1.EditingControl.Text); break;
    //                case 6: tmpZoom.AckRate = Convert.ToDouble(dataGridView1.EditingControl.Text); break;
    //                default: break;
    //            }
    //            //新项填入
    //            zoomInfo.Insert(dataGridView1.CurrentCell.RowIndex, tmpZoom);
    //            //删除list中的旧项
    //            zoomInfo.Remove(zoomInfo[dataGridView1.CurrentCell.RowIndex-1]);
    //        }
    //        else
    //        {
    //            switch (dataGridView1.CurrentCell.ColumnIndex)
    //            {
    //                case 0: tmpZoom.ZoomNum = Convert.ToInt16(dataGridView1.EditingControl.Text); tmpZoom.count |= 0x01; break;
    //                case 1: tmpZoom.ZoomName = dataGridView1.EditingControl.Text; tmpZoom.count|=0x02; break;
    //                case 2: tmpZoom.ZoomStartLoc = Convert.ToDouble(dataGridView1.EditingControl.Text); tmpZoom.count |= 0x04; break;
    //                case 3: tmpZoom.ZoomEndLoc = Convert.ToDouble(dataGridView1.EditingControl.Text); tmpZoom.count |= 0x08; break;
    //                case 4: tmpZoom.Threshold = Convert.ToDouble(dataGridView1.EditingControl.Text); tmpZoom.count |= 0x10; break;
    //                case 5: tmpZoom.ExpRate = Convert.ToDouble(dataGridView1.EditingControl.Text); tmpZoom.count |= 0x20; break;
    //                case 6: tmpZoom.AckRate = Convert.ToDouble(dataGridView1.EditingControl.Text); tmpZoom.count |= 0x40; break;
    //                default: break;
    //            }
    //        }
    //        if ((tmpZoom.count |0x00000000 ) == 0x7f)
    //        {
    //            tmpZoom.count = 0;
    //            zoomInfo.Add(tmpZoom);
    //        }
    //}

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //将配置参数写入
            foreach (var tmp in zoomInfo)
            {
                if (tmp.ZoomNum != Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].FormattedValue))//检查主键重复
                    zoomInfo.Add(tmp);
                else
                {
                    zoomInfo.Clear();
                    MessageBox.Show("区间编号有重复，请修改编号，避免重复！");
                    break;
                }
            }
        }

    }
}
