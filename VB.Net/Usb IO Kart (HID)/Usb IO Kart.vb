Imports UsbLibrary
Imports System.Text

Public Class Usb_IO_Kart





    Dim Usb_1 As New UsbHidPort
    Dim Usb_2 As New UsbHidPort
    ' Projenin Target CPU x86 Olmalıdır. Çünkü usblibrary x86 Desteklemektedir.
    Enum Cihaz_Komut
        CihazTest = 206
        ReadSerialNo = 207
        WriteSerialNo = 208
        ReadDeviceModel = 209
        ParameterWrite = 210
        ResetDevice = 211
        DeviceBootMode = 212
        ReadDeviceFirmware = 213
    End Enum
    Protected Overloads Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        MyBase.OnHandleCreated(e)
        Usb_1.RegisterHandle(Handle)
        Usb_2.RegisterHandle(Handle)
    End Sub
    Protected Overloads Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            Usb_1.ParseMessages(m)
            Usb_2.ParseMessages(m)
            MyBase.WndProc(m)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Usb1_Baglanan_Cihaz(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Usb1_Cihaz_Durumu_Label.Text = "Bağlı"
        Usb1_Cihaz_Durumu_Label.ForeColor = Color.Green

        Dim Popup As New NotifyIcon
        Popup.Icon = My.Resources._957
        Popup.Visible = True
        Popup.ShowBalloonTip(500, "Bilgi ", "Usb 1 Bağlı ! ", ToolTipIcon.Info)
        Popup.Visible = False
        Popup.Dispose()
    End Sub
    Private Sub Usb1_Cıkan_Cihaz(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If InvokeRequired Then
            Invoke(New EventHandler(AddressOf Usb1_Cıkan_Cihaz), New Object() {sender, e})
        End If
    End Sub
    Private Sub Usb1_OnSpecifiedDeviceArrived(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub Usb1_OnSpecifiedDeviceRemoved(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Usb1_Cihaz_Durumu_Label.Text = "Bağlı Değil"
        Usb1_Cihaz_Durumu_Label.ForeColor = Color.Red
        Usb1_input_1_Picture.Image = My.Resources.Disconnect
        Usb1_input_2_Picture.Image = My.Resources.Disconnect
        Usb1_input_3_Picture.Image = My.Resources.Disconnect
        Usb1_input_4_Picture.Image = My.Resources.Disconnect

        Usb1_Role_1_Picture.Image = My.Resources.Disconnect
        Usb1_Role_2_Picture.Image = My.Resources.Disconnect
        Usb1_Role_3_Picture.Image = My.Resources.Disconnect
        Usb1_Role_4_Picture.Image = My.Resources.Disconnect


        Dim Popup As New NotifyIcon
        Popup.Icon = My.Resources._957
        Popup.Visible = True
        Popup.ShowBalloonTip(500, "Bilgi ", "Usb 1 Çıkarıldı ! ", ToolTipIcon.Warning)
        Popup.Visible = False
        Popup.Dispose()
    End Sub
    Private Sub Usb1_Data_Oku(ByVal senders As System.Object, ByVal Argsc As UsbLibrary.DataRecievedEventArgs)
        Try
            Dim GelenData As String = ""
            If InvokeRequired Then
                Invoke(New DataRecievedEventHandler(AddressOf Usb1_Data_Oku), New Object() {senders, Argsc})
            Else

                Dim GelenByteDizesi(65) As Byte
                GelenByteDizesi = Argsc.data
                If GelenByteDizesi(1) = 35 Then ' # ise...

                    For i = 2 To 64
                        Dim Dt As Byte = GelenByteDizesi(i)
                        If Dt = 0 Then
                            Exit For
                        End If
                        GelenData &= Chr(Dt)
                    Next

                    If GelenData.Contains("|") AndAlso GelenData.Length = 13 Then
                        Usb1_Role_1_Suresi_Text.Text = CInt(GelenData.Substring(1, 3))
                        Usb1_Role_2_Suresi_Text.Text = CInt(GelenData.Substring(4, 3))
                        Usb1_Role_3_Suresi_Text.Text = CInt(GelenData.Substring(7, 3))
                        Usb1_Role_4_Suresi_Text.Text = CInt(GelenData.Substring(10, 3))

                    Else
                        MsgBox(GelenData)
                    End If



                Else

                    For i = 1 To 8
                        GelenData &= GelenByteDizesi(i).ToString()
                    Next
                    If GelenData.Length = 8 Then
                        If GelenData.Substring(0, 1) = "1" Then
                            Usb1_input_1_Picture.Image = My.Resources.Connect
                        Else
                            Usb1_input_1_Picture.Image = My.Resources.Disconnect
                        End If

                        If GelenData.Substring(1, 1) = "1" Then
                            Usb1_input_2_Picture.Image = My.Resources.Connect
                        Else
                            Usb1_input_2_Picture.Image = My.Resources.Disconnect
                        End If

                        If GelenData.Substring(2, 1) = "1" Then
                            Usb1_input_3_Picture.Image = My.Resources.Connect
                        Else
                            Usb1_input_3_Picture.Image = My.Resources.Disconnect
                        End If
                        If GelenData.Substring(3, 1) = "1" Then
                            Usb1_input_4_Picture.Image = My.Resources.Connect
                        Else
                            Usb1_input_4_Picture.Image = My.Resources.Disconnect
                        End If

                        If GelenData.Substring(4, 1) = "1" Then
                            Usb1_Role_1_Picture.Image = My.Resources.Connect
                        Else
                            Usb1_Role_1_Picture.Image = My.Resources.Disconnect
                        End If

                        If GelenData.Substring(5, 1) = "1" Then
                            Usb1_Role_2_Picture.Image = My.Resources.Connect
                        Else
                            Usb1_Role_2_Picture.Image = My.Resources.Disconnect
                        End If

                        If GelenData.Substring(6, 1) = "1" Then
                            Usb1_Role_3_Picture.Image = My.Resources.Connect
                        Else
                            Usb1_Role_3_Picture.Image = My.Resources.Disconnect
                        End If

                        If GelenData.Substring(7, 1) = "1" Then
                            Usb1_Role_4_Picture.Image = My.Resources.Connect
                        Else
                            Usb1_Role_4_Picture.Image = My.Resources.Disconnect
                        End If

                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Sub Usb2_Baglanan_Cihaz(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Usb2_Cihaz_Durumu_Label.Text = "Bağlı"
        Usb2_Cihaz_Durumu_Label.ForeColor = Color.Green


        Dim Popup As New NotifyIcon
        Popup.Icon = My.Resources._957
        Popup.Visible = True
        Popup.ShowBalloonTip(500, "Bilgi ", "Usb 2 Bağlandı ! ", ToolTipIcon.Info)
        Popup.Visible = False
        Popup.Dispose()
    End Sub
    Private Sub Usb2_Cıkan_Cihaz(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If InvokeRequired Then
            Invoke(New EventHandler(AddressOf Usb2_Cıkan_Cihaz), New Object() {sender, e})
        End If
    End Sub
    Private Sub Usb2_OnSpecifiedDeviceArrived(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub Usb2_OnSpecifiedDeviceRemoved(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Usb2_Cihaz_Durumu_Label.Text = "Bağlı Değil"
        Usb2_Cihaz_Durumu_Label.ForeColor = Color.Red
        Usb2_input_1_Picture.Image = My.Resources.Disconnect
        Usb2_input_2_Picture.Image = My.Resources.Disconnect
        Usb2_input_3_Picture.Image = My.Resources.Disconnect
        Usb2_input_4_Picture.Image = My.Resources.Disconnect

        Usb2_Role_1_Picture.Image = My.Resources.Disconnect
        Usb2_Role_2_Picture.Image = My.Resources.Disconnect
        Usb2_Role_3_Picture.Image = My.Resources.Disconnect
        Usb2_Role_4_Picture.Image = My.Resources.Disconnect

        Dim Popup As New NotifyIcon
        Popup.Icon = My.Resources._957
        Popup.Visible = True
        Popup.ShowBalloonTip(500, "Bilgi ", "Usb 2 Çıkarıldı ! ", ToolTipIcon.Warning)
        Popup.Visible = False
        Popup.Dispose()


    End Sub
    Private Sub Usb2_Data_Oku(ByVal sender As System.Object, ByVal Args As UsbLibrary.DataRecievedEventArgs)
        Dim GelenData As String = ""
        If InvokeRequired Then
            Invoke(New DataRecievedEventHandler(AddressOf Usb2_Data_Oku), New Object() {sender, Args})
        Else

            Dim GelenByteDizesi(8) As Byte
            GelenByteDizesi = Args.data
            For i = 1 To 8
                GelenData = GelenData & GelenByteDizesi(i).ToString()
            Next


            If GelenData.Length = 8 Then
                If GelenData.Substring(0, 1) = "1" Then
                    Usb2_input_1_Picture.Image = My.Resources.Connect
                Else
                    Usb2_input_1_Picture.Image = My.Resources.Disconnect
                End If

                If GelenData.Substring(1, 1) = "1" Then
                    Usb2_input_2_Picture.Image = My.Resources.Connect
                Else
                    Usb2_input_2_Picture.Image = My.Resources.Disconnect
                End If

                If GelenData.Substring(2, 1) = "1" Then
                    Usb2_input_3_Picture.Image = My.Resources.Connect
                Else
                    Usb2_input_3_Picture.Image = My.Resources.Disconnect
                End If
                If GelenData.Substring(3, 1) = "1" Then
                    Usb2_input_4_Picture.Image = My.Resources.Connect
                Else
                    Usb2_input_4_Picture.Image = My.Resources.Disconnect
                End If

                If GelenData.Substring(4, 1) = "1" Then
                    Usb2_Role_1_Picture.Image = My.Resources.Connect
                Else
                    Usb2_Role_1_Picture.Image = My.Resources.Disconnect
                End If

                If GelenData.Substring(5, 1) = "1" Then
                    Usb2_Role_2_Picture.Image = My.Resources.Connect
                Else
                    Usb2_Role_2_Picture.Image = My.Resources.Disconnect
                End If

                If GelenData.Substring(6, 1) = "1" Then
                    Usb2_Role_3_Picture.Image = My.Resources.Connect
                Else
                    Usb2_Role_3_Picture.Image = My.Resources.Disconnect
                End If

                If GelenData.Substring(7, 1) = "1" Then
                    Usb2_Role_4_Picture.Image = My.Resources.Connect
                Else
                    Usb2_Role_4_Picture.Image = My.Resources.Disconnect
                End If
            End If
        End If
    End Sub

    Private Sub Usb_IO_Kart_Yonetim_Formu_Load(sender As Object, e As EventArgs) Handles MyBase.Load





        Usb_1.VendorId = 1590
        Usb_1.ProductId = 1590

        AddHandler Usb_1.OnSpecifiedDeviceArrived, AddressOf Usb1_OnSpecifiedDeviceArrived
        AddHandler Usb_1.OnSpecifiedDeviceRemoved, AddressOf Usb1_OnSpecifiedDeviceRemoved
        AddHandler Usb_1.OnDeviceArrived, AddressOf Usb1_Baglanan_Cihaz
        AddHandler Usb_1.OnDeviceRemoved, AddressOf Usb1_Cıkan_Cihaz
        AddHandler Usb_1.OnDataRecieved, AddressOf Usb1_Data_Oku

        If Usb_1.CheckDevicePresent() = True Then
            Usb1_Cihaz_Durumu_Label.Text = "Bağlı"
            Usb1_Cihaz_Durumu_Label.ForeColor = Color.Green
        Else
            Usb1_Cihaz_Durumu_Label.Text = "Bağlı Değil"
            Usb1_Cihaz_Durumu_Label.ForeColor = Color.Red
        End If
        '_______________________________________________________________________


        Usb_2.VendorId = 1591
        Usb_2.ProductId = 1591

        AddHandler Usb_2.OnSpecifiedDeviceArrived, AddressOf Usb2_OnSpecifiedDeviceArrived
        AddHandler Usb_2.OnSpecifiedDeviceRemoved, AddressOf Usb2_OnSpecifiedDeviceRemoved
        AddHandler Usb_2.OnDeviceArrived, AddressOf Usb2_Baglanan_Cihaz
        AddHandler Usb_2.OnDeviceRemoved, AddressOf Usb2_Cıkan_Cihaz
        AddHandler Usb_2.OnDataRecieved, AddressOf Usb2_Data_Oku

        If Usb_2.CheckDevicePresent() = True Then
            Usb2_Cihaz_Durumu_Label.Text = "Bağlı"
            Usb2_Cihaz_Durumu_Label.ForeColor = Color.Green
        Else
            Usb2_Cihaz_Durumu_Label.Text = "Bağlı Değil"
            Usb2_Cihaz_Durumu_Label.ForeColor = Color.Red
        End If
    End Sub

    Private Sub Sorgu_Timer_Tick(sender As Object, e As EventArgs) Handles Sorgu_Timer.Tick


        If Durum_Sorgula_Check_1.Checked = True Then
            Dim ByteDize(64) As Byte
            ByteDize(1) = 64 ' 64 Sorgu Komutudur ve ASCII ' Q Harfine Denk Gelir Qestion=Soru

            If Usb_1.SpecifiedDevice IsNot Nothing Then
                If Usb_1.SpecifiedDevice IsNot Nothing Then
                    Usb_1.SpecifiedDevice.SendData(ByteDize)
                End If
            End If
        End If

        If Durum_Sorgula_Check_2.Checked = True Then
            Dim ByteDize(64) As Byte
            ByteDize(1) = 64 ' 64 Sorgu Komutudur ve ASCII ' Q Harfine Denk Gelir Qestion=Soru
            If Usb_2.SpecifiedDevice IsNot Nothing Then
                If Usb_2.SpecifiedDevice IsNot Nothing Then
                    Usb_2.SpecifiedDevice.SendData(ByteDize)
                End If
            End If
        End If

    End Sub


    Private Sub Usb1_Role_1_Tetik_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_1_Tetik_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 10 '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Role_1_Cek_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_1_Cek_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 11                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Role_1_Bırak_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_1_Bırak_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                 '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 12                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Input_1_Alındı_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Input_1_Alındı_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                 '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 51                         '1.Dijit İnput Durum Bildirme Kodudur.  2.Dijit Hangi İnput' a Ait  işlem Yapılacağını Bildirir.
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Bazı Durumlarda Okunan input Bilgisinin Bir Defa Gelmesini İsteriz. Bu Komut Gönderildiğinde İnput 1,2,3,4 Aktif olsa bile Aktif olduğuna dair bilgi Gelmez.
            '                                         Not 2:İnput Pasif Olup Tekrar Aktif Olduğunda İnput Bilgisi vermeye devam eder..
        End If
    End Sub

    Private Sub Usb1_Role_2_Tetik_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_2_Tetik_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                   '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 20                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Role_2_Cek_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_2_Cek_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                   '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 21                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Role_2_Bırak_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_2_Bırak_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 22                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Input_2_Alındı_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Input_2_Alındı_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 52                         '1.Dijit İnput Durum Bildirme Kodudur.  2.Dijit Hangi İnput' a Ait  işlem Yapılacağını Bildirir.
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Bazı Durumlarda Okunan input Bilgisinin Bir Defa Gelmesini İsteriz. Bu Komut Gönderildiğinde İnput 1,2,3,4 Aktif olsa bile Aktif olduğuna dair bilgi Gelmez.
            '                                         Not 2:İnput Pasif Olup Tekrar Aktif Olduğunda İnput Bilgisi vermeye devam eder..
        End If
    End Sub

    Private Sub Usb1_Role_3_Tetik_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_3_Tetik_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                   '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 30                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Role_3_Cek_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_3_Cek_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 31                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Role_3_Bırak_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_3_Bırak_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                   '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 32                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Input_3_Alındı_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Input_3_Alındı_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                   '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 53                         '1.Dijit İnput Durum Bildirme Kodudur.  2.Dijit Hangi İnput' a Ait  işlem Yapılacağını Bildirir.
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Bazı Durumlarda Okunan input Bilgisinin Bir Defa Gelmesini İsteriz. Bu Komut Gönderildiğinde İnput 1,2,3,4 Aktif olsa bile Aktif olduğuna dair bilgi Gelmez.
            '                                         Not 2:İnput Pasif Olup Tekrar Aktif Olduğunda İnput Bilgisi vermeye devam eder..
        End If
    End Sub

    Private Sub Usb1_Role_4_Tetik_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_4_Tetik_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                 '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 40                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Role_4_Cek_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_4_Cek_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 41                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Role_4_Bırak_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Role_4_Bırak_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte               '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 42                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb1_Input_4_Alındı_Buton_Click(sender As Object, e As EventArgs) Handles Usb1_Input_4_Alındı_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 54                         '1.Dijit İnput Durum Bildirme Kodudur.  2.Dijit Hangi İnput' a Ait  işlem Yapılacağını Bildirir.
            Usb_1.SpecifiedDevice.SendData(ByteDize) 'Not : Bazı Durumlarda Okunan input Bilgisinin Bir Defa Gelmesini İsteriz. Bu Komut Gönderildiğinde İnput 1,2,3,4 Aktif olsa bile Aktif olduğuna dair bilgi Gelmez.
            '                                         Not 2:İnput Pasif Olup Tekrar Aktif Olduğunda İnput Bilgisi vermeye devam eder..
        End If
    End Sub

    Private Sub Usb2_Role_1_Tetik_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_1_Tetik_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte               '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 10                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Role_1_Cek_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_1_Cek_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 11                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Role_1_Bırak_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_1_Bırak_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 12                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Input_1_Alındı_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Input_1_Alındı_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 51                         '1.Dijit İnput Durum Bildirme Kodudur.  2.Dijit Hangi İnput' a Ait  işlem Yapılacağını Bildirir.
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Bazı Durumlarda Okunan input Bilgisinin Bir Defa Gelmesini İsteriz. Bu Komut Gönderildiğinde İnput 1,2,3,4 Aktif olsa bile Aktif olduğuna dair bilgi Gelmez.
            '                                         Not 2:İnput Pasif Olup Tekrar Aktif Olduğunda İnput Bilgisi vermeye devam eder..
        End If
    End Sub

    Private Sub Usb_Role_2_Tetik_Buton_Click(sender As Object, e As EventArgs) Handles Usb_Role_2_Tetik_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 20                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Role_2_Cek_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_2_Cek_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 21                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Role_2_Bırak_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_2_Bırak_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 22                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Input_2_Alındı_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Input_2_Alındı_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                 '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 52                         '1.Dijit İnput Durum Bildirme Kodudur.  2.Dijit Hangi İnput' a Ait  işlem Yapılacağını Bildirir.
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Bazı Durumlarda Okunan input Bilgisinin Bir Defa Gelmesini İsteriz. Bu Komut Gönderildiğinde İnput 1,2,3,4 Aktif olsa bile Aktif olduğuna dair bilgi Gelmez.
            '                                         Not 2:İnput Pasif Olup Tekrar Aktif Olduğunda İnput Bilgisi vermeye devam eder..
        End If
    End Sub

    Private Sub Usb2_Role_3_Tetik_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_3_Tetik_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                   '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 30                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Role_3_Cek_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_3_Cek_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 31                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Role_3_Bırak_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_3_Bırak_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte               '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 32                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Input_3_Alındı_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Input_3_Alındı_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 53                         '1.Dijit İnput Durum Bildirme Kodudur.  2.Dijit Hangi İnput' a Ait  işlem Yapılacağını Bildirir.
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Bazı Durumlarda Okunan input Bilgisinin Bir Defa Gelmesini İsteriz. Bu Komut Gönderildiğinde İnput 1,2,3,4 Aktif olsa bile Aktif olduğuna dair bilgi Gelmez.
            '                                         Not 2:İnput Pasif Olup Tekrar Aktif Olduğunda İnput Bilgisi vermeye devam eder..
        End If
    End Sub

    Private Sub Usb2_Role_4_Tetik_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_4_Tetik_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 40                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Role_4_Cek_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_4_Cek_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 41                        '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Role_4_Bırak_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Role_4_Bırak_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                  '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 42                         '2 Dijit Değer Gönderilir. 1.Dijit Röle Numarasıdır.  2. Dijit ise Röle işlem Tipidir. 0=Tetikleme ,1=Röleyi Süreli Aç ,2=Röleyi Kapat
            Usb_2.SpecifiedDevice.SendData(ByteDize) ' Not : Röle Aç Komutunu Alan Röle Tetikleme Kodu Gönderilse Dahi Tetikleme Yapmaz. Yani Röle Otomatik Olarak Kapanmaz.
        End If
    End Sub

    Private Sub Usb2_Input_4_Alındı_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Input_4_Alındı_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte                '8 Bytelık Bir Dize Oluşturulur ve 1.Byte Dizesine  Değer Yazılır.
            ByteDize(1) = 54                         '1.Dijit İnput Durum Bildirme Kodudur.  2.Dijit Hangi İnput' a Ait  işlem Yapılacağını Bildirir.
            Usb_2.SpecifiedDevice.SendData(ByteDize) 'Not : Bazı Durumlarda Okunan input Bilgisinin Bir Defa Gelmesini İsteriz. Bu Komut Gönderildiğinde İnput 1,2,3,4 Aktif olsa bile Aktif olduğuna dair bilgi Gelmez.
            '                                         Not 2:İnput Pasif Olup Tekrar Aktif Olduğunda İnput Bilgisi vermeye devam eder..
        End If
    End Sub


    Private Sub Role_Surelerini_Ayarla_Buton_Click(sender As Object, e As EventArgs) Handles Role_Surelerini_Ayarla_Buton.Click



        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte
            ByteDize(1) = Asc("R")
            ByteDize(2) = Asc("T")
            ByteDize(3) = Usb1_Role_1_Suresi_Text.Text
            ByteDize(4) = Usb1_Role_2_Suresi_Text.Text
            ByteDize(5) = Usb1_Role_3_Suresi_Text.Text
            ByteDize(6) = Usb1_Role_4_Suresi_Text.Text

            Usb_1.SpecifiedDevice.SendData(ByteDize)
        End If

    End Sub


    Private Sub Usb2_Buton_Role_Ayarla_Buton_Click(sender As Object, e As EventArgs) Handles Usb2_Buton_Role_Ayarla_Buton.Click
        If Usb_2.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte
            ByteDize(1) = Asc("R")
            ByteDize(2) = Asc("T")
            ByteDize(3) = Usb2_Role_1_Suresi_Text.Text
            ByteDize(4) = Usb2_Role_2_Suresi_Text.Text
            ByteDize(5) = Usb2_Role_3_Suresi_Text.Text
            ByteDize(6) = Usb2_Role_4_Suresi_Text.Text

            Usb_2.SpecifiedDevice.SendData(ByteDize)
        End If
    End Sub













    Private Sub Hangi_Cihaz_Buton_Click(sender As Object, e As EventArgs) Handles Hangi_Cihaz_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte
            ByteDize(1) = Cihaz_Komut.CihazTest
            Usb_1.SpecifiedDevice.SendData(ByteDize)


        End If
    End Sub

    Private Sub Boot_Mode_Buton_Click(sender As Object, e As EventArgs) Handles Boot_Mode_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte
            ByteDize(1) = Cihaz_Komut.DeviceBootMode
            Usb_1.SpecifiedDevice.SendData(ByteDize)
            '                                         
        End If
    End Sub

    Private Sub Model_Oku_Buton_Click(sender As Object, e As EventArgs) Handles Model_Oku_Buton.Click

        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte
            ByteDize(1) = Cihaz_Komut.ReadDeviceModel
            Usb_1.SpecifiedDevice.SendData(ByteDize)
            '                                         
        End If
    End Sub

    Private Sub Versiyon_Oku_Buton_Click(sender As Object, e As EventArgs) Handles Versiyon_Oku_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte
            ByteDize(1) = Cihaz_Komut.ReadDeviceFirmware
            Usb_1.SpecifiedDevice.SendData(ByteDize)
        End If

    End Sub

    Private Sub Seri_No_Yaz_Buton_Click(sender As Object, e As EventArgs) Handles Seri_No_Yaz_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then

            Dim SerialNo() As Char = Guid.NewGuid.ToString.Replace("-", "").ToString.ToUpper.ToCharArray
            Dim ByteDize(64) As Byte
            ByteDize(1) = Cihaz_Komut.WriteSerialNo
            For i = 0 To SerialNo.Count - 1
                ByteDize(2 + i) = Asc(SerialNo(i))
            Next


            Usb_1.SpecifiedDevice.SendData(ByteDize)
        End If
    End Sub

    Private Sub SeriNo_Oku_Buton_Click(sender As Object, e As EventArgs) Handles SeriNo_Oku_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte
            ByteDize(1) = Cihaz_Komut.ReadSerialNo
            Usb_1.SpecifiedDevice.SendData(ByteDize)
        End If
    End Sub

    Private Sub Role_Surelerini_Oku_Buton_Click(sender As Object, e As EventArgs) Handles Role_Surelerini_Oku_Buton.Click
        If Usb_1.SpecifiedDevice IsNot Nothing Then
            Dim ByteDize(64) As Byte
            ByteDize(1) = Asc("T")
            ByteDize(2) = Asc("R")
            Usb_1.SpecifiedDevice.SendData(ByteDize)
        End If
    End Sub
End Class
