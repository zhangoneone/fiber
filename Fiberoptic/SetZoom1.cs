using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace FiberopticServer
{
    public partial class SetZoom1 : Form
    {
        DataTable dt;
        DataRow dr;
        importdll.ZoomInfo tmpZoom = new importdll.ZoomInfo();
        public static List<importdll.ZoomInfo> zoomInfo = new List<importdll.ZoomInfo>();
        public SetZoom1()
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
            dt.Columns.Add("阈值设置");
            dt.Columns.Add("异常比设置%");
            dt.Columns.Add("确认比设置%");
            dataGridView1.DataSource = dt;
            toolStripButton2.Enabled = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            zoomInfo.Clear();//原来的清空
            dt.Clear();
            dataGridView1.Update();
            if (ZoomParam_From_Xml(zoomInfo) == -1)
            {
                MessageBox.Show("无默认配置！");
                return;
            }
            //清除配置文件，重新写入
          //  ZoomParam_Clear_Xml();
            //显示到页面上
            int MaxRow = zoomInfo.Count;//行数
            int i,j=0;
            for (i = 0; i < MaxRow; i++)
            {
                try
                {
                    dr = dt.NewRow();
                    dr[j++] = zoomInfo[i].ZoomNum;
                    dr[j++] = zoomInfo[i].ZoomName;
                    dr[j++] = zoomInfo[i].ZoomStartLoc;
                    dr[j++] = zoomInfo[i].ZoomEndLoc;
                    dr[j++] = zoomInfo[i].Threshold;
                    dr[j++] = zoomInfo[i].ExpRate;
                    dr[j++] = zoomInfo[i].AckRate;
                    dt.Rows.Add(dr);
                    j = 0;
                }
                catch(Exception ex) { MessageBox.Show(ex.ToString()+"读取配置出错！"); }
            }
            toolStripButton2.Enabled = true;
            //写入到程序中
            //if (ZoomParamSet() == 0)
            //{
            //    MessageBox.Show("加载配置成功！");
            //}
            //else
            //{
            //    MessageBox.Show("加载配置失败！请检查配置文件！");
            //}
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            zoomInfo.Clear();
            //将配置参数写入
            int i, j = 0;
            int MaxRow, MaxCol;
            MaxRow = dataGridView1.RowCount;
            MaxCol = dataGridView1.ColumnCount;
            for (i = 0; i < MaxRow - 1; i++)
            {
                if (!zoomInfo.Exists(x => x.ZoomNum == Convert.ToInt32(dataGridView1[0, i].FormattedValue)))//list中不存在该项
                {
                    try
                    {
                        tmpZoom.ZoomNum = Convert.ToInt32(dataGridView1[j++, i].EditedFormattedValue);
                        tmpZoom.ZoomName = dataGridView1[j++, i].EditedFormattedValue.ToString();
                        tmpZoom.ZoomStartLoc = Convert.ToDouble(dataGridView1[j++, i].EditedFormattedValue);
                        tmpZoom.ZoomEndLoc = Convert.ToDouble(dataGridView1[j++, i].EditedFormattedValue);
                        tmpZoom.Threshold = Convert.ToDouble(dataGridView1[j++, i].EditedFormattedValue);
                        tmpZoom.ExpRate = Convert.ToDouble(dataGridView1[j++, i].EditedFormattedValue);
                        tmpZoom.AckRate = Convert.ToDouble(dataGridView1[j++, i].EditedFormattedValue);
                        zoomInfo.Add(tmpZoom);
                        j = 0;
                    }
                    catch { j = 0; MessageBox.Show("区间参数请填写完整！"); }
                }
                else
                {
                    zoomInfo.Clear();
                    MessageBox.Show("区间编号有重复，请修改编号，避免重复！");
                    break;
                }
                zoomInfo.Sort((x, y) => x.ZoomNum < y.ZoomNum ? -1 : 0);//num升序排列

            }
            //报警参数重置为默认参数
            importdll.ZoomClear_function();
            //写入程序
            if (ZoomParamSet() == 0)
            {
                MessageBox.Show("写入完成！");
            }
            else
            {
                MessageBox.Show("写入失败！请检查参数设置!");
            }
            // 清除配置文件，重新写入
            ZoomParam_Clear_Xml();
            //写入xml
            ZoomParam_2_Xml(zoomInfo);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }


        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            switch (dataGridView1.CurrentCell.ColumnIndex)
            {
                case 0: try { Convert.ToInt16(dataGridView1.EditingControl.Text); }
                    catch { MessageBox.Show("请输入一个整数！"); } break;
                case 1: try { Convert.ToString(dataGridView1.EditingControl.Text); }
                    catch { MessageBox.Show("请输入一个字符串！"); } break;
                case 2: try { Convert.ToDouble(dataGridView1.EditingControl.Text); }
                    catch { MessageBox.Show("请输入一个小数！"); } break;
                case 3: try { Convert.ToDouble(dataGridView1.EditingControl.Text); }
                    catch { MessageBox.Show("请输入一个小数！"); } break;
                case 4: try { Convert.ToDouble(dataGridView1.EditingControl.Text); }
                    catch { MessageBox.Show("请输入一个小数！"); } break;
                case 5: try { Convert.ToDouble(dataGridView1.EditingControl.Text); }
                    catch { MessageBox.Show("请输入一个小数！"); } break;
                case 6: try { Convert.ToDouble(dataGridView1.EditingControl.Text); }
                    catch { MessageBox.Show("请输入一个小数！"); } break;
                default: break;
            }
            toolStripButton2.Enabled = true;
        }
        //
        private importdll.ZoomInfo[] ZoomParam_Converse(List<importdll.ZoomInfo> zoomInfo)
        {
            importdll.ZoomInfo[] tmpZoom_Array = zoomInfo.ToArray();
            for (int i = 0; i < tmpZoom_Array.Length; i++)
            {
                tmpZoom_Array[i].ZoomStartLoc /= (ServerForm.scantlv / 2);
                tmpZoom_Array[i].ZoomEndLoc /= (ServerForm.scantlv / 2);
            }
            return tmpZoom_Array;
        }
        public int ZoomParamSet()
        {
            IntPtr ptr;
            int ret=0;
            try
            {
                importdll.ZoomInfo[] tmp = ZoomParam_Converse(zoomInfo);
                //数组 托管内存->非托管内存
                int size = (int)(Marshal.SizeOf(tmp[0]) * tmp.Length);//获取数组占用空间大小
                ptr = Marshal.AllocHGlobal(size * tmp.Length);//声明一个同样大小的空间

                //结构体 托管内存->非托管内存
                foreach (var b in tmp)
                {
                    Marshal.StructureToPtr(b, ptr, true);//将结构体放到这个空间中
                    //执行函数，设置参数
                    ret = importdll.ZoomSet_function(ptr, tmp.Length);
                }
            }
            catch { return -1; }
            return ret;
        }
        public void ZoomParam_2_Xml(List<importdll.ZoomInfo> zoominfo)
        {
           Xmlopera xml=new Xmlopera();
           xml.Create_Zoom_Xml();
           foreach (var b in zoominfo)
           {
               xml.Append_Zoom_Xml(b);
           }
        }
        public int ZoomParam_From_Xml(List<importdll.ZoomInfo> zoominfo)
        {
            Xmlopera xml=new Xmlopera();
            return xml.ReadXml(zoominfo);
        }
        public void ZoomParam_Clear_Xml()
        {
            Xmlopera xml = new Xmlopera();
            xml.Delete_Xml();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ZoomParam_Clear_Xml();
            MessageBox.Show("清除成功！");
        }
    }
}
