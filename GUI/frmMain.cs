using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DTO;
using BLL;

namespace GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        Dictionary<string, CASY> dsCaSy = null;
        Dictionary<string, BAIHAT> dsBaiHat = null;
        string csDangChon;
        int vitriBH = 0;//lưu vị trí bài hát đang hiển thị trong Ds Bài Hát của CsDangChon.
        bool Thembtn = false;
        bool Suabtn = false;      
        /// <summary>
        /// Hàm này bất/tắt chế độ readOnly textbox.
        /// </summary>
        private void ChoPhepNhap(bool battat)
        {
            txtMaBaiHat.ReadOnly = battat;
            txtTenBaiHat.ReadOnly = battat;
            txtTacGia.ReadOnly = battat;
        }
        private void LayToanBoDuLieu()
        {
            LayToanBoCaSy();
            LayToanBoBaiHat();
            //Săp xếp các bài hát vào đúng với ca sỹ trình bài.
            foreach(KeyValuePair<string,CASY> cs in dsCaSy)
            {
                foreach(KeyValuePair<string,BAIHAT> bh in dsBaiHat)
                {
                    if (bh.Value.CaSy.MaCS == cs.Key)
                        cs.Value.dsBaiHat.Add(bh.Value);
                }
            }
        }

        private void LayToanBoBaiHat()
        {
            dsBaiHat = new Dictionary<string, BAIHAT>();
            BAIHATBLL bhbll = new BAIHATBLL();
            foreach(BAIHAT bh in bhbll.LayToanBoBaiHat())
            {
                dsBaiHat.Add(bh.MaBH, bh);
            }
        }

        private void LayToanBoCaSy()
        {
            dsCaSy = new Dictionary<string, CASY>();
            CASYBLL casybll = new CASYBLL();
            foreach(CASY cs in casybll.LayToanBoCaSy())
            {
                dsCaSy.Add(cs.MaCS, cs);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LayToanBoDuLieu();
            HienThiCaSylenComboBox();
            ChoPhepNhap(true);     
        }

        private void HienThiCaSylenComboBox()
        {
            cboCaSy.Items.Clear();
            foreach (CASY cs in dsCaSy.Values.ToList())
            {
                cboCaSy.Items.Add(cs.TenCS);
            }
        }

        private void cboCaSy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCaSy.SelectedIndex == -1)
                return;
            string tenCS = cboCaSy.Text;
            foreach(CASY cs in dsCaSy.Values.ToList())
            {
                if (cs.TenCS == tenCS)
                {
                    csDangChon = cs.MaCS;                  
                    HienThiThongTinBaiHat(dsCaSy[csDangChon].dsBaiHat[0]);
                    break;
                }        
            }
        }

        private void HienThiThongTinBaiHat(BAIHAT bh)
        {
            lblcasy.Text = "Danh sách bài hát của ca sỹ: " + dsCaSy[csDangChon].TenCS;
            lblViTri.Text = (vitriBH + 1) + "/" + dsCaSy[csDangChon].dsBaiHat.Count;
            txtMaBaiHat.Text = bh.MaBH;
            txtTenBaiHat.Text = bh.TenBH;
            txtTacGia.Text = bh.NhacSy;
        }

        private void btnFrist_Click(object sender, EventArgs e)
        {
            vitriBH = 0;
            HienThiThongTinBaiHat(dsCaSy[csDangChon].dsBaiHat[vitriBH]);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            vitriBH = dsCaSy[csDangChon].dsBaiHat.Count - 1;
            HienThiThongTinBaiHat(dsCaSy[csDangChon].dsBaiHat[vitriBH]);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (vitriBH < dsCaSy[csDangChon].dsBaiHat.Count-1)
            {
                vitriBH++;
                HienThiThongTinBaiHat(dsCaSy[csDangChon].dsBaiHat[vitriBH]);
            }
                
        }

        private void btnPrivious_Click(object sender, EventArgs e)
        {
            if (vitriBH > 0)
            {
                vitriBH--;
                HienThiThongTinBaiHat(dsCaSy[csDangChon].dsBaiHat[vitriBH]);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(Thembtn == false)
            {
                if (cboCaSy.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn ca sỹ", "Thông báo");
                    return;
                }
                else
                {
                    txtMaBaiHat.Text = "";
                    txtTenBaiHat.Text = "";
                    txtTacGia.Text = "";
                    Thembtn = true;
                    Suabtn = false;
                    ChoPhepNhap(false);
                    btnThem.BackColor = Color.Green;
                    btnSua.BackColor = Color.WhiteSmoke;
                }
            }
            else
            {
                Thembtn = false;
                btnThem.BackColor = Color.WhiteSmoke;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KiemTraThongTinLuu())
            {
                if (Thembtn == true)
                {
                    LuuThem();
                    Thembtn = false;
                    btnThem.BackColor = Color.WhiteSmoke;
                }
                else if (Suabtn == true)
                {
                    LuuSua();
                    Suabtn = false;
                    btnSua.BackColor = Color.WhiteSmoke;
                }
                else
                {
                    MessageBox.Show("Chọn chế độ lưu hoặc thêm trước.");
                }
            }
            else
            {
                MessageBox.Show("Hãy kiểm tra lại thông tin.");
            } 
        }

        private void LuuSua()
        {
            BAIHAT bh = new BAIHAT();
            bh.MaBH = txtMaBaiHat.Text;
            bh.TenBH = txtTenBaiHat.Text;
            bh.NhacSy = txtTacGia.Text;
            bh.CaSy = dsCaSy[csDangChon];
            BAIHATBLL bhbll = new BAIHATBLL();
            bhbll.CapNhatBaiHat(bh);
            LayToanBoDuLieu();
            HienThiThongTinBaiHat(bh);           
        }

        private void LuuThem()
        {
            BAIHAT bh = new BAIHAT();
            bh.MaBH = txtMaBaiHat.Text;
            bh.TenBH = txtTenBaiHat.Text;
            bh.NhacSy = txtTacGia.Text;
            bh.CaSy = dsCaSy[csDangChon];
            ThemBaiHatXuongCSDL(bh);
            LayToanBoDuLieu();
            vitriBH = dsCaSy[csDangChon].dsBaiHat.Count - 1;
            HienThiThongTinBaiHat(dsCaSy[csDangChon].dsBaiHat[vitriBH]);
        }

        private void ThemBaiHatXuongCSDL(BAIHAT bh)
        {
            BAIHATBLL bhbll = new BAIHATBLL();
            bhbll.LuuBaiHat(bh);
        }

        private bool KiemTraThongTinLuu()
        {
            if (txtMaBaiHat.Text == "" || txtTenBaiHat.Text == "" || txtTacGia.Text == "")
            {
                return false;
            }
                
            return true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (Suabtn == false)
            {
                ChoPhepNhap(false);
                txtMaBaiHat.ReadOnly = true;
                Suabtn = true;
                Thembtn = false;
                btnSua.BackColor = Color.Green;
                btnThem.BackColor = Color.WhiteSmoke;
            }
            else
            {
                Suabtn = false;
                btnSua.BackColor = Color.WhiteSmoke;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaBaiHat.Text == "")
            {
                MessageBox.Show("Chưa có bài hát nào được chọn.");
            }
            else
            {
                BAIHATBLL bhbll = new BAIHATBLL();
                bhbll.XoaBaiHat(txtMaBaiHat.Text);
                MessageBox.Show("Đã xóa!");
                LayToanBoDuLieu();
                vitriBH = 0;
                HienThiThongTinBaiHat(dsCaSy[csDangChon].dsBaiHat[vitriBH]);
            }
        }
    }
}
