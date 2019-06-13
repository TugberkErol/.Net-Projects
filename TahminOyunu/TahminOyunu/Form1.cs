using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
namespace TahminOyunu
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        int[] answer1 = new int[4];
        int numb1 = 0, numb2 = 0, numb3 = 0, numb4 = 0;
        int  total2 = 0, total3 = 0;
            
            //HashTable'da 0-9 sayılarını ve bu sayıların ağırlığını tutacağız.
            Hashtable hashtable = new Hashtable();
            hashtable.Add(0, 0);
            hashtable.Add(1, 0);
            hashtable.Add(2, 0);
            hashtable.Add(3, 0);
            hashtable.Add(4, 0);
            hashtable.Add(5, 0);
            hashtable.Add(6, 0);
            hashtable.Add(7, 0);
            hashtable.Add(8, 0);
            hashtable.Add(9, 0);
            int userValue = 0;

            //Kullanıcının girdiği sayıyı integera parse ediyoruz.
           
            Int32.TryParse(textBox1.Text, out userValue);

            if(userValue<1000 || userValue > 9999)
            {
                MessageBox.Show("4 basamaklı bir sayı giriniz...");
                Application.Exit();
            }
            
            //Kullanıcının girdiği değerin her bir rakamını diziye atayalım.
            int[] inputArray = new int[4];

            //Kullanıcı girişinin basamaklarına ayrılıyor.
          
            
            inputArray[0] = userValue / 1000;
            inputArray[1] = (userValue % 1000) / 100;
            inputArray[2] = (userValue % 100) / 10;
            inputArray[3] = userValue % 10;
            

            if (inputArray[0] == 0)
            {
                MessageBox.Show("İlk basamak 0 olamaz tekrar deneyiniz");
                Application.Exit();

            }

            if (inputArray[0]==inputArray[1] || inputArray[0] == inputArray[2] || inputArray[0] == inputArray[3] ||
                inputArray[1] == inputArray[2] || inputArray[1] == inputArray[3] || inputArray[2] == inputArray[3] )
            {
                MessageBox.Show("Rakamlar birbirinden farklı olmalıdır.");
                Application.Exit();
                
            }

            //İlerde kullanıcı ile yazılımın tuttuğu sayıları karşılaştırabilmek için diziyi int yapalım.
            int user = Int32.Parse(inputArray[0] + "" + inputArray[1] + "" + inputArray[2] + "" + inputArray[3]);

            //Yazılımın en başta tuttuğu değere atama yapalım.
            int[] softwareRandom = new int[4];
            softwareRandom[0] = 1;
            softwareRandom[1] = 0;
            softwareRandom[2] = 2;
            softwareRandom[3] = 3;

            //Totaldeki amaç projede ipucu olarak tanımlanan basamakları aynı ve birbirine eşit sayılar için (+1) aynı sayılar fakat basamak değeri 
            //farklı sayılar için (-1) ipucu verilecek.Biz burda öncelikle hem yazılımın hemde kullanıcının girdiği rakamların aynı olmasına bakalım
            //örnek olarak yazılım=1234 kullanıcı=3412 olsun. Burada ikisinin tüm rakamları aynı ama sırası değişik olduğu için (2) ve (-2) dönmesi
            //isteriz.(ipucu için) Ama burada biz 4 döndereceğiz ( 2 + |(-2)|)=4 en sonda rakamların yerlerini değiştirerek tahmin  yapacağız.
                int total = 0;

            //while döngüsü yazılımdaki rakamların hepsi kullanıcıdaki rakamlarda olduğu zaman while döngüsü bitecek.
            while (total < 4) {
                total = 0;
               
                int index0 = softwareRandom[0];
                int index1 = softwareRandom[1];
                int index2 = softwareRandom[2];
                int index3 = softwareRandom[3];


                //Buradaki döngüde totali hesaplıyoruz.
                for (int i = 0; i < 4; i++)
                {
                    for(int j = 0;  j < 4; j++)
                    {
                        if (inputArray[j] == softwareRandom[i])
                        {
                            total++;
                        }
                    }
                }
                
                //eğer yazılımın tahmin ettiği sayıdaki rakamlardan hiçbiri kullanıcının yazdığı rakamlardan hiçbirini içermiyorsa
                //yazılımın tahmin ettiği sayıdaki rakamlar hashtable dan çıkarırız.
                if (total == 0)
                {
                    hashtable.Remove(index0);
                    hashtable.Remove(index1);
                    hashtable.Remove(index2);
                    hashtable.Remove(index3);

                }
                //total=1 ise ipuçları ile kullanıcının tuttuğu sayıda olmayan 3 rakamı buluruz ve hashtable dan çıkarırırız.Aynı zamanda
                //kullanıcının tuttuğu sayıdaki 1 rakamı kesin olarak buluruz.
                else if (total == 1)
                {
                    if ((inputArray.Contains(index0) || inputArray.Contains(index1) || inputArray.Contains(index2))==false) { hashtable.Remove(index0); hashtable.Remove(index1); hashtable.Remove(index2); }
                    else if ((inputArray.Contains(index0) || inputArray.Contains(index1) || inputArray.Contains(index3))==false) { hashtable.Remove(index0); hashtable.Remove(index1); hashtable.Remove(index3); }
                    else if ((inputArray.Contains(index1) || inputArray.Contains(index2) || inputArray.Contains(index3))==false) { hashtable.Remove(index1); hashtable.Remove(index2); hashtable.Remove(index3); }

                    total2 = 10000000;

                    if (hashtable.ContainsKey(index0)){  hashtable[index0] =  total2; }
                    if (hashtable.ContainsKey(index1)){  hashtable[index1] =  total2; }
                    if (hashtable.ContainsKey(index2)){  hashtable[index2] =  total2; }
                    if (hashtable.ContainsKey(index3)){  hashtable[index3] =  total2;  }




                }
                //Eğer yazılımın tahmin ettiği sayıdaki rakamlardan 2 tanesi kullanıcının yazdığı sayıda varsa hashtable'da yazılımın girdiği
                //rakamların keyleri bulunur ve bu keylerin ağırlıklarını azaltırız.
                
                else if (total == 2)
                {
                    total3 = -1;
                    int old1 = (int)hashtable[index0];
                    int old2 = (int)hashtable[index1];
                    int old3 = (int)hashtable[index2];
                    int old4 = (int)hashtable[index3];
                    hashtable[index0] = old1+total3;
                    hashtable[index1] = old2+total3;
                    hashtable[index2] = old3+total3;
                    hashtable[index3] = old4+total3;
                }
                //Eğer yazılımın tahmin ettiği sayıdaki rakamlardan 3 tanesi kullanıcının yazdığı sayıda varsa yazılımdaki 4 basamaklı 
                //sayılardan sadece 1 tanesi yok demektir.Bunu da kullanıcıya sora sora buluruz.Böylece tüm aynı rakamları buluruz ve while 
                //döngüsü tamamlanır.
                else if (total == 3)
                {
                    if(inputArray.Contains(index0) && inputArray.Contains(index1) && inputArray.Contains(index2)) {  hashtable.Remove(index3); softwareRandom[0]= index0; softwareRandom[1] = index1; softwareRandom[2] = index2;  }
                    else if (inputArray.Contains(index0) && inputArray.Contains(index1) && inputArray.Contains(index3)) { hashtable.Remove(index2); softwareRandom[0] = index0; softwareRandom[1] = index1; softwareRandom[2] = index3;  }
                    else if (inputArray.Contains(index0) && inputArray.Contains(index2) && inputArray.Contains(index3)) { hashtable.Remove(index1); softwareRandom[0] = index0; softwareRandom[1] = index2; softwareRandom[2] = index3; }
                    else if (inputArray.Contains(index1) && inputArray.Contains(index2) && inputArray.Contains(index3)) { hashtable.Remove(index0); softwareRandom[0] = index1; softwareRandom[1] = index2; softwareRandom[2] = index3;  }
                    for(int i = 0; i < 10; i++)
                    {
                        if(( inputArray.Contains(i) && softwareRandom[0]!=i && softwareRandom[1]!= i && softwareRandom[2] != i)) { softwareRandom[3] = i; Console.WriteLine("4.konumu "+i ); }
                    }
                    break;
                }

                int max = -2147483648;
                //Bu for döngüsünde yazılımın ilk rakamı seçmesi sağlanıyor
                //HashTable dan en yüksek ağırlıklı key'i rakam olarak seçecek.
                for (int i = 0; i < 10; i++)
                {
               
                
                    if (hashtable.ContainsKey(i))
                    {
                        int value = (int)hashtable[i];
                        if (value > max)
                        {
                            numb1 = i;

                            max = (int)hashtable[i];

                        }
                    }
               

                }
                //Bu for döngüsünde yazılımın ikinci rakamı seçmesi sağlanıyor
                //HashTable dan en yüksek ikinci ağırlıklı key'i rakam olarak seçecek.
                max = -2147483648;
                for (int i = 0; i < 10; i++)
            {
                    if (hashtable.ContainsKey(i))
                    {
                        if (i == numb1)
                        {
                            continue;
                        }
                        else if ((int)hashtable[i] > max)
                        {
                            numb2 = i;
                            max = (int)hashtable[i];

                        }
                    }
            }
                //Bu for döngüsünde yazılımın üçüncü rakamı seçmesi sağlanıyor
                //HashTable dan en yüksek üçüncü ağırlıklı key'i rakam olarak seçecek.
                max = -2147483648;
                for (int i = 0; i < 10; i++)
            {
                    if (hashtable.ContainsKey(i))
                    {
                        if (i == numb2 || i == numb1)
                        {

                            continue;
                        }
                        else if ((int)hashtable[i] > max)
                        {
                            numb3 = i;
                            max = (int)hashtable[i];

                        }
                    }
            }
                //Bu for döngüsünde yazılımın dördüncü rakamı seçmesi sağlanıyor
                //HashTable dan en yüksek dördüncü ağırlıklı key'i rakam olarak seçecek.
                max = -2147483648;
                for (int i = 0; i < 10; i++)
            {
                    if (hashtable.ContainsKey(i))
                    {

                        if (i == numb3 || i == numb2 || i == numb1)
                        {
                            continue;
                        }
                        else if ((int)hashtable[i] > max)
                        {
                            numb4 = i;
                            max = (int)hashtable[i];

                        }
                    }
            }

                //Yeni rakamlar yazılımın tahmin dizisine atanıyor.
                softwareRandom[0] = numb1;
                softwareRandom[1] = numb2;
                softwareRandom[2] = numb3;
                softwareRandom[3] = numb4;       
            }
            //While döngüsü bittikten sonra yazılımın tahmin ettiği sayı ile kullanıcının girdiği sayının rakamları aynı olacak
            //Ama sırası farklı olacağı için biz bu ihtimalleri hesaplayacağız.En kötü durumda 24 hamlede bitecek.En iyi durum 1 seferde.

            int[] last = new int[24];
            last[0] = Int32.Parse(softwareRandom[0] + "" + softwareRandom[1] + "" + softwareRandom[2] + "" + softwareRandom[3]);
            last[1] = Int32.Parse(softwareRandom[0] + "" + softwareRandom[1] + "" + softwareRandom[3] + "" + softwareRandom[2]);
            last[2] = Int32.Parse(softwareRandom[0] + "" + softwareRandom[2] + "" + softwareRandom[1] + "" + softwareRandom[3]);
            last[3] = Int32.Parse(softwareRandom[0] + "" + softwareRandom[2] + "" + softwareRandom[3] + "" + softwareRandom[1]);
            last[4] = Int32.Parse(softwareRandom[0] + "" + softwareRandom[3] + "" + softwareRandom[1] + "" + softwareRandom[2]);
            last[5] = Int32.Parse(softwareRandom[0] + "" + softwareRandom[3] + "" + softwareRandom[2] + "" + softwareRandom[1]);
            last[6] = Int32.Parse(softwareRandom[1] + "" + softwareRandom[0] + "" + softwareRandom[2] + "" + softwareRandom[3]);
            last[7] = Int32.Parse(softwareRandom[1] + "" + softwareRandom[0] + "" + softwareRandom[3] + "" + softwareRandom[2]);
            last[8] = Int32.Parse(softwareRandom[1] + "" + softwareRandom[2] + "" + softwareRandom[0] + "" + softwareRandom[3]);
            last[9] = Int32.Parse(softwareRandom[1] + "" + softwareRandom[2] + "" + softwareRandom[3] + "" + softwareRandom[0]);
            last[10] = Int32.Parse(softwareRandom[1] + "" + softwareRandom[3] + "" + softwareRandom[0] + "" + softwareRandom[2]);
            last[11] = Int32.Parse(softwareRandom[1] + "" + softwareRandom[3] + "" + softwareRandom[2] + "" + softwareRandom[0]);
            last[12] = Int32.Parse(softwareRandom[2] + "" + softwareRandom[0] + "" + softwareRandom[1] + "" + softwareRandom[3]);
            last[13] = Int32.Parse(softwareRandom[2] + "" + softwareRandom[0] + "" + softwareRandom[3] + "" + softwareRandom[1]);
            last[14] = Int32.Parse(softwareRandom[2] + "" + softwareRandom[1] + "" + softwareRandom[0] + "" + softwareRandom[3]);
            last[15] = Int32.Parse(softwareRandom[2] + "" + softwareRandom[1] + "" + softwareRandom[3] + "" + softwareRandom[0]);
            last[16] = Int32.Parse(softwareRandom[2] + "" + softwareRandom[3] + "" + softwareRandom[0] + "" + softwareRandom[1]);
            last[17] = Int32.Parse(softwareRandom[2] + "" + softwareRandom[3] + "" + softwareRandom[1] + "" + softwareRandom[0]);
            last[18] = Int32.Parse(softwareRandom[3] + "" + softwareRandom[0] + "" + softwareRandom[1] + "" + softwareRandom[2]);
            last[19] = Int32.Parse(softwareRandom[3] + "" + softwareRandom[0] + "" + softwareRandom[2] + "" + softwareRandom[1]);
            last[20] = Int32.Parse(softwareRandom[3] + "" + softwareRandom[1] + "" + softwareRandom[0] + "" + softwareRandom[2]);
            last[21] = Int32.Parse(softwareRandom[3] + "" + softwareRandom[1] + "" + softwareRandom[2] + "" + softwareRandom[0]);
            last[22] = Int32.Parse(softwareRandom[3] + "" + softwareRandom[2] + "" + softwareRandom[0] + "" + softwareRandom[1]);
            last[23] = Int32.Parse(softwareRandom[3] + "" + softwareRandom[2] + "" + softwareRandom[1] + "" + softwareRandom[0]);

            //Burada son işlem olarak kullanıcının girdiği sayı bulunur.
            for (int i = 0; i < 24; i++)
            {
                if(last[i]==user){
                    MessageBox.Show(last[i].ToString());
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] software = new int[4];
            software[0] = 1;
            software[1] = 6;
            software[2] = 9;
            software[3] = 2;

            int userValue, total = 0, total2 = 0;
            Int32.TryParse(textBox1.Text, out userValue);

            //Kullanıcının girdiği değerin her bir rakamını diziye atayalım.
            int[] inputArray = new int[4];

            inputArray[0] = userValue / 1000;
            inputArray[1] = (userValue % 1000) / 100;
            inputArray[2] = (userValue % 100) / 10;
            inputArray[3] = userValue % 10;
            
            //Burada kullanıcının tahmin ettiği sayı için geri dönüş olarak ipucu veriyoruz.
            for(int i=0;i<4;i++)
            {
                if (software[i] == inputArray[i]) { total++; }
                else if(software[i]!=inputArray[i] && inputArray.Contains(software[i]))
                {
                    total2--;
                }
            }
            if (total == 4)
            {
                MessageBox.Show("Tebrikler Doğru Cevap");
            }

           label2.Text="("+total + ")" + "----(" + total2 + ")";

        }
    }
}
