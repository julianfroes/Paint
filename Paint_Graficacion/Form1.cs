namespace Paint_Graficacion
{
    public partial class Graficacion_Unidad_2 : Form
    {
        SolidBrush brocha = new SolidBrush(Color.Red);
        Font fuente;
        //arriba el america
        Bitmap bm,seleccion,seleccionC;
        Bitmap[] bmaps = new Bitmap[100];
        Image[] capas = new Image[100];
        Rectangle rectangulo;
        RectangleF rectanguloclon;
        Graphics g;
        bool paint = false,rehacer=false;
        Point px, py,p1,p2,p3,p4,p5,p6,pr=new Point(1,1);
        Point[] puntos=new Point[6];
        Pen p = new Pen(Color.Black, 1);
        Pen borrador = new Pen(Color.White, 10);
        Pen borradorfondo = new Pen(Color.White, 1);
        int opcion,bmcantidad=0,nonulos=0;
        int x, y, punto2X, punto2Y, puntoiX, puntoiY, puntofinalX1,puntofinalY1, puntofinalX2, puntofinalY2, Ltriangulo = 0, pInicialX=-1,pInicialY;
        float promXY;
        RectangleF recbm;
        Size tamaño;




        ColorDialog cd = new ColorDialog();
        ColorDialog cdfondo = new ColorDialog();

        Color new_color,lienzo_color,viejocolorfondo;

        private void button2_Click(object sender, EventArgs e)
        {
            opcion = 3;
        }

        private void Belpise_Click(object sender, EventArgs e)
        {
            opcion = 4;
        }

        private void Brectangulo_Click(object sender, EventArgs e)
        {
            opcion = 5;
        }

        private void Lienzo_MouseLeave(object sender, EventArgs e)
        {
            x = 0;
            y = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            opcion = 10;
        }

        private void Bseleccionar_Click(object sender, EventArgs e)
        {
            opcion = 15;
        }

        private void Bborrar_Click(object sender, EventArgs e)
        {
            SolidBrush borrabrocha = new SolidBrush (borrador.Color);
            g.FillRectangle(borrabrocha, rectangulo);
            Guardacambios();
            Lienzo.Refresh();
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sib_Click(object sender, EventArgs e)
        {
            //g.DrawImage(capas[bmcantidad], pr);
            if (bmcantidad > 0)
            {
                bmcantidad--;
                
                g.DrawImage(capas[bmcantidad], pr);
                Lienzo.Refresh();
            }
            

        }

        private void Lienzo_Click(object sender, EventArgs e)
        {
            if (opcion == 10)
            {
                p1 = new Point(puntoiX, puntoiY);
            }
        }

        private void nob_Click(object sender, EventArgs e)
        {
            bmcantidad++;
            
            if(bmcantidad <= nonulos )
            {
                g.DrawImage(capas[bmcantidad], pr);
                Lienzo.Refresh();
                rehacer = true;
            }
            else
            {
                bmcantidad--;
            }
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            viejocolorfondo = borrador.Color;
            cdfondo.ShowDialog();
            new_color = cdfondo.Color;
            cuadroFondo.BackColor = cdfondo.Color;
            
            borrador.Color = cdfondo.Color;
            borradorfondo.Color = cdfondo.Color;
            Lienzo.BackColor = cdfondo.Color;

            for (int ix = 1; ix < bm.Width; ix++)
            {
                for (int iy = 1; iy < bm.Height; iy++)
                {
                    if(bm.GetPixel(ix, iy).ToArgb() == viejocolorfondo.ToArgb()) { bm.SetPixel(ix, iy, cdfondo.Color); }

                    
                }
            }
            Lienzo.Refresh();


        }

        private void Bpegar_Click(object sender, EventArgs e)
        {
            if(seleccionC != null)
            {
                g.DrawImage(seleccionC, p1);
                Guardacambios();
                Lienzo.Refresh();
            }
           

        }

        private void Bcopiar_Click(object sender, EventArgs e)
        {
            if(rectanguloclon.Width != 0 || rectanguloclon.Height != 0)
            {
                System.Drawing.Imaging.PixelFormat format = bm.PixelFormat;
                seleccionC = bm.Clone(rectanguloclon, format);
                Bpegar.Enabled = true;
            }
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(nutamañofuente.Value > 48)
            {
                nutamañofuente.Value = 48;
            }
            else if(nutamañofuente.Value < 8)
            {
                nutamañofuente.Value = 8;
            }
        }

        private void Bguardar_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0,0,Lienzo.Width,Lienzo.Height),bm.PixelFormat);
                //btm.Save(sfd.FileName,ImageFormat.Jpeg);
                btm.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void Babrir_Click(object sender, EventArgs e)
        {
            var dibujo = new OpenFileDialog();
            var resultado = dibujo.ShowDialog();
            if(resultado == DialogResult.Cancel)
            {
                return;
            }
            var filename = dibujo.FileName;
            var image = Image.FromFile(filename);
            //Lienzo.Image = image;
            g.DrawImage(image,0,0);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            opcion = 11;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            opcion = 6;
        }

        private void Lienzo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (paint&&(x>0 && y>0)&&(Lienzo.Height>=y&&Lienzo.Width>=x))
            {
                if (opcion == 3)
                {

                    if ((punto2X < 0 && punto2Y < 0) || (punto2X > 0 && punto2Y > 0)) promXY = (punto2X + punto2Y) / 2;
                    if (punto2X > 0 && punto2Y < 0) { promXY = (punto2X - punto2Y) / 2; }
                    if (punto2X < 0 && punto2Y > 0) { promXY = (punto2Y - punto2X) / 2; }
                    //x- y-
                    if (punto2X < 0 && punto2Y < 0) 
                    {
                        if ((puntoiX + promXY) > 0 && (puntoiY + promXY) > 0)
                        {
                            g.DrawEllipse(p, puntoiX, puntoiY, promXY, promXY);
                        }
                    }
                    //x+ y-
                    if (punto2X > 0 && punto2Y < 0) 
                    {
                        if (puntoiX + promXY < Lienzo.Width && (puntoiY - promXY) > 0)
                        {
                            g.DrawEllipse(p, puntoiX, puntoiY - promXY, promXY, promXY);
                        }
                         
                    }
                    //x- y+
                    if (punto2X < 0 && punto2Y > 0) 
                    {
                        if (puntoiX - promXY > 0 && (puntoiY + promXY) < Lienzo.Height)
                        {
                            g.DrawEllipse(p, puntoiX - promXY, puntoiY, promXY, promXY);
                        }
                    }
                    //x+ y+ 
                    if (punto2X > 0 && punto2Y > 0) 
                    {

                        if ((puntoiX + promXY) < Lienzo.Width && (puntoiY + promXY) < Lienzo.Height)
                        {
                                g.DrawEllipse(p, puntoiX, puntoiY, promXY, promXY);
                        }
                        
                    }

                }
                if (opcion == 4)
                {
                    g.DrawEllipse(p, puntoiX, puntoiY, punto2X, punto2Y);
                }
                if (opcion == 5)
                {
                    if (punto2X < 0 && punto2Y < 0) { g.DrawRectangle(p, puntoiX + punto2X, puntoiY + punto2Y, punto2X * -1, punto2Y * -1); }
                    if (punto2X > 0 && punto2Y < 0) { g.DrawRectangle(p, puntoiX, puntoiY + punto2Y, punto2X, punto2Y * -1); }
                    if (punto2X < 0 && punto2Y > 0) { g.DrawRectangle(p, puntoiX + punto2X, puntoiY, punto2X * -1, punto2Y); }
                    if (punto2X > 0 && punto2Y > 0) { g.DrawRectangle(p, puntoiX, puntoiY, punto2X, punto2Y); }
                    
                }
                if (opcion == 6)
                {
                    g.DrawLine(p, puntoiX, puntoiY, x, y);
                    
                }
                if (opcion == 7)
                {

                    if (Ltriangulo < 1)
                    {
                        g.DrawLine(p, puntoiX, puntoiY, x, y);
                        pInicialX = puntoiX;
                        pInicialY = puntoiY;
                    }
                    if(Ltriangulo >= 1)
                    {

                        p3 = new Point( x, y);
                        //puntofinalX2 = puntofinalX1 + x;
                        //puntofinalY2 = puntofinalY1 + y;
                        g.DrawLine(p, p2,p3);

                    }
                    
                }
                
                if( opcion == 11)
                {
                    p1 = new Point(puntoiX, (int)(puntoiY + (punto2Y * 0.5)));
                    p2 = new Point((int)(puntoiX + (punto2X * 0.33)), puntoiY);
                    p3 = new Point((int)(puntoiX + (punto2X * 0.66)), puntoiY);
                    p4 = new Point(puntoiX + punto2X, (int)(puntoiY + (punto2Y * 0.5)));
                    p5 = new Point((int)(puntoiX + (punto2X * 0.66)), puntoiY + punto2Y);
                    p6 = new Point((int)(puntoiX + (punto2X * 0.33)), puntoiY + punto2Y);
                    puntos[0] = p1;
                    puntos[1] = p2;
                    puntos[2] = p3;
                    puntos[3] = p4;
                    puntos[4] = p5;
                    puntos[5] = p6;

                    g.DrawPolygon(p, puntos);
                }
                if(opcion == 15)
                {
                    g.DrawRectangle(p, puntoiX, puntoiY, punto2X, punto2Y);
                    rectangulo.X = puntoiX;
                    rectangulo.Y = puntoiY;
                    rectangulo.Width = punto2X;
                    rectangulo.Height = punto2Y;
                    p1 = new Point(puntoiX, puntoiY);
                    
                }

            }
        }

        private void Blimpiar_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            Lienzo.Image = bm;
            opcion = 0;
            cdfondo.Color = Color.White;
            cuadroFondo.BackColor = cdfondo.Color;
            borrador.Color = cdfondo.Color;
            Lienzo.BackColor = cdfondo.Color;
            Guardacambios();
        }

        private void PaletaColores_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = set_point(PaletaColores, e.Location);
            PaletaColores.BackColor=((Bitmap)PaletaColores.Image).GetPixel(point.X, point.Y);
            new_color = PaletaColores.BackColor;
            p.Color = PaletaColores.BackColor;
            cuadroColor.BackColor = p.Color;
        }

        private void ColorS_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            cuadroColor.BackColor = cd.Color;
            p.Color= cd.Color;
        }
        static Point set_point(PictureBox pb,Point pt)
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * pX), (int)(pt.Y * pY));
        }
        private void validate(Bitmap bm, Stack<Point>sp,int x,int y,Color old_color,Color new_color)
        {
            Color cx = bm.GetPixel(x,y);
            if (cx == old_color)
            {
                sp.Push(new Point(x,y));
                bm.SetPixel(x, y, new_color);
            }
        }

        private void Lienzo_MouseClick(object sender, MouseEventArgs e)
        {
            if(opcion == 8)
            {
                Point point = set_point(Lienzo,e.Location);
                Fill(bm, point.X, point.Y, new_color);
                Guardacambios();
            }
            if (opcion == 10)
            {
                fuente = new Font(cFuentes.Text, (float)nutamañofuente.Value);
                brocha = new SolidBrush(p.Color);
                g.DrawString(cajatexto.Text, fuente, brocha, p1);
                Guardacambios();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            opcion = 8;
        }

        private void Btriangulo_Click(object sender, EventArgs e)
        {
            opcion = 7;
        }

        public void Fill(Bitmap bm, int x,int y, Color new_clr)
        {
            Color old_color = bm.GetPixel(x,y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x,y));
            bm.SetPixel(x,y,new_clr);
            if (old_color == new_clr) return;
            
            while(pixel.Count > 0)
            {
                Point pt = (Point)pixel.Pop();
                if (pt.X>0 && pt.Y>0 && pt.X<bm.Width-1 && pt.Y<bm.Height-1)
                {
                    validate(bm,pixel,pt.X-1,pt.Y,old_color,new_color);
                    validate(bm, pixel, pt.X, pt.Y - 1, old_color, new_color);
                    validate(bm, pixel, pt.X + 1, pt.Y, old_color, new_color);
                    validate(bm, pixel, pt.X, pt.Y + 1, old_color, new_color);
                }
            }
        }
        public Graficacion_Unidad_2()
        {
            InitializeComponent();


            cFuentes.Text = "Arial";

            bm = new Bitmap(Lienzo.Width, Lienzo.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            Lienzo.Image = bm;
            recbm = new RectangleF(0,0, Lienzo.Width,Lienzo.Height);

            System.Drawing.Imaging.PixelFormat format = bm.PixelFormat;
            var image = bm.Clone(recbm, format);
            capas[bmcantidad] = image;
        }

        private void Lienzo_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if(opcion == 1)
                {
                    px = e.Location;
                    g.DrawLine(p, px,py);
                    py = px;
                }
                if (opcion ==2)
                {
                    px = e.Location;
                    g.DrawLine(borrador, px, py);
                    py = px;
                }
                
            }
            Lienzo.Refresh();

            x = e.X;
            y = e.Y;
            punto2X = e.X - puntoiX;
            punto2Y = e.Y - puntoiY;
        }

        private void Lienzo_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;

            puntoiX = e.X;
            puntoiY = e.Y;
        }

        private void Lienzo_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            if((x > 0 && y > 0) && (Lienzo.Height >= y && Lienzo.Width >= x))
            {
                punto2X = x - puntoiX;
                punto2Y = y - puntoiY;
                if(opcion==1 || opcion == 2) { Guardacambios(); }
                if (opcion == 3)
                {
                    if (punto2X < 0 && punto2Y < 0)
                    {
                        if ((puntoiX + promXY) > 0 && (puntoiY + promXY) > 0)
                        {
                            g.DrawEllipse(p, puntoiX, puntoiY, promXY, promXY);
                        }
                    }
                    //x+ y-
                    if (punto2X > 0 && punto2Y < 0)
                    {
                        if (puntoiX + promXY < Lienzo.Width && (puntoiY - promXY) > 0)
                        {
                            g.DrawEllipse(p, puntoiX, puntoiY - promXY, promXY, promXY);
                        }

                    }
                    //x- y+
                    if (punto2X < 0 && punto2Y > 0)
                    {
                        if (puntoiX - promXY > 0 && (puntoiY + promXY) < Lienzo.Height)
                        {
                            g.DrawEllipse(p, puntoiX - promXY, puntoiY, promXY, promXY);
                        }
                    }
                    //x+ y+ 
                    if (punto2X > 0 && punto2Y > 0)
                    {

                        if ((puntoiX + promXY) < Lienzo.Width && (puntoiY + promXY) < Lienzo.Height)
                        {
                            g.DrawEllipse(p, puntoiX, puntoiY, promXY, promXY);
                        }

                    }
                    Guardacambios();
                    
                    

                }
                if (opcion == 4)
                {
                    g.DrawEllipse(p, puntoiX, puntoiY, punto2X, punto2Y);
                    Guardacambios();
                }
                if (opcion == 5)
                {

                    if (punto2X < 0 && punto2Y < 0) { g.DrawRectangle(p, puntoiX + punto2X, puntoiY + punto2Y, punto2X * -1, punto2Y * -1); }
                    if (punto2X > 0 && punto2Y < 0) { g.DrawRectangle(p, puntoiX, puntoiY + punto2Y, punto2X, punto2Y * -1); }
                    if (punto2X < 0 && punto2Y > 0) { g.DrawRectangle(p, puntoiX + punto2X, puntoiY, punto2X * -1, punto2Y); }
                    if (punto2X > 0 && punto2Y > 0) { g.DrawRectangle(p, puntoiX, puntoiY, punto2X, punto2Y); }
                    Guardacambios();


                }
                if (opcion == 6)
                {
                    g.DrawLine(p, puntoiX, puntoiY, x, y);
                    Guardacambios();

                }
                if (opcion == 7)
                {

                    if (pInicialX == -1)
                    {

                    }
                    else
                    {
                        Ltriangulo++;
                        if (Ltriangulo == 1)
                        {
                            puntofinalX1 = puntoiX + punto2X;
                            puntofinalY1 = puntoiY + punto2Y;
                            pInicialX = puntoiX;
                            pInicialY = puntoiY;
                            g.DrawLine(p, puntoiX, puntoiY, x, y);
                            p1 = new Point(pInicialX, pInicialY);
                            p2 = new Point(puntofinalX1, puntofinalY1);
                            paint = true;
                            p3 = p2;
                        }
                        else if (Ltriangulo > 1)
                        {




                            //p3 = new Point(puntofinalX2, puntofinalY2);
                            g.DrawLine(p, p2, p3);

                            g.DrawLine(p, p1, p3);
                            Guardacambios();
                            Ltriangulo = 0;
                            puntofinalX1 = 0;
                            puntofinalY1 = 0;
                            pInicialX = 0;
                            pInicialY = 0;
                            puntofinalX2 = 0;
                            puntofinalY2 = 0;
                            //p1 = new Point(0,0);
                            //p2 = new Point(0, 0);
                            //p3 = new Point(0, 0);
                            paint = false;
                        }

                    }
                }
                if (opcion == 10)
                {
                    //seleccion = (Bitmap)Clipboard.GetImage();
                    //g.DrawImageUnscaledAndClipped(bm,rectangulo);
                }
                if (opcion == 11)
                {
                    g.DrawPolygon(p, puntos);
                    Guardacambios();
                }
                if(opcion == 15)
                {
                    if (Math.Abs(punto2X) > 1 && Math.Abs(punto2Y) > 1)
                    {
                        seleccion = new Bitmap(Math.Abs(punto2X), Math.Abs(punto2Y));
                        rectanguloclon = rectangulo;
                        Bcopiar.Enabled = true;
                        Bborrar.Enabled = true;

                    }
                }
            

            }
        }

        private void Bborrador_Click(object sender, EventArgs e)
        {
            opcion = 2;
        }

        private void Bmano_Click(object sender, EventArgs e)
        {
            opcion = 1;
        }
        public void Guardacambios()
        {
            bmcantidad++;
            nonulos++;
            System.Drawing.Imaging.PixelFormat format = bm.PixelFormat;
            var image = bm.Clone(recbm, format);
            capas[bmcantidad] = image;
            if (rehacer)
            {
                nonulos = bmcantidad;
            }
            
            
        }
    }
}