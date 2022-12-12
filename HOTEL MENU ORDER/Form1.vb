Imports System.Drawing.Printing

Public Class Form1
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim LongPaper As Integer
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        cmbMenu.Text = " "
        txtQuantity.Text = " "
        txtTotal.Text = " "
        txtUnitPrice.Text = " "
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        Dim items() As Integer = {20, 30, 20, 50, 100, 150, 50, 50, 35} 'DECLARES FOOD PRICES

        If cmbMenu.SelectedIndex = 0 Then 'IF UGALI IS SELECTED

            txtUnitPrice.Text = Val(items(0))

        ElseIf cmbMenu.SelectedIndex = 1 Then 'IF RICE IS SELECTED
            txtUnitPrice.Text = Val(items(1))

        ElseIf cmbMenu.SelectedIndex = 2 Then 'IF CHAPATI IS SELECTED
            txtUnitPrice.Text = Val(items(2))

        ElseIf cmbMenu.SelectedIndex = 3 Then 'IF BEANS IS SELECTED
            txtUnitPrice.Text = Val(items(3))

        ElseIf cmbMenu.SelectedIndex = 4 Then 'IF BEEF IS SELECTED
            txtUnitPrice.Text = Val(items(4))

        ElseIf cmbMenu.SelectedIndex = 5 Then 'IF KUKU IS SELECTED
            txtUnitPrice.Text = Val(items(5))

        ElseIf cmbMenu.SelectedIndex = 6 Then 'IF SODA IS SELECTED
            txtUnitPrice.Text = Val(items(6))

        ElseIf cmbMenu.SelectedIndex = 7 Then 'IF SAUSAGES IS SELECTED
            txtUnitPrice.Text = Val(items(7))

        ElseIf cmbMenu.SelectedIndex = 8 Then 'IF SMOKIE IS SELECTED
            txtUnitPrice.Text = Val(items(8))



        End If


    End Sub

    Private Sub btnTotal_Click(sender As Object, e As EventArgs) Handles btnTotal.Click
        Dim UnitPrice, Quantity, Total As Integer
        UnitPrice = txtUnitPrice.Text
        Quantity = txtQuantity.Text
        Total = UnitPrice * Quantity
        txtTotal.Text = Val(Total)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        dgvList.Rows.Add(cmbMenu.Text, txtQuantity.Text, txtTotal.Text)

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)

    End Sub

    Private Sub PD_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles PD.BeginPrint
        Dim PageSetup As New PageSettings
        PageSetup.PaperSize = New PaperSize("custom", 250, 500) 'Determines the size of the receipt
        PD.DefaultPageSettings = PageSetup

    End Sub
    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        'DECLARING DIFFERENT FONTS FOR DIFFERENT THINGS IN THE RECEIPT AS VARIABLES, TO BE 
        'CALLED LATER ON
        Dim Font8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim Font8Italic As New Font("Comic Sans MS", 8, FontStyle.Italic)
        Dim Font10Bold As New Font("Calibri", 10, FontStyle.Bold)
        Dim Font14 As New Font("Calibri", 14, FontStyle.Bold)

        'DECLARING MARGINS
        Dim LeftMargin As Integer = PD.DefaultPageSettings.Margins.Left
        Dim CentreMargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim RightMargin As Integer = PD.DefaultPageSettings.PaperSize.Width


        'FONT ALIGNMENT 
        Dim Right As New StringFormat
        Dim centre As New StringFormat
        Right.Alignment = StringAlignment.Far
        centre.Alignment = StringAlignment.Center


        Dim Line As String
        Line = "====================================================="

        e.Graphics.DrawString("KWA MATHE PLACE", Font14, Brushes.Black, CentreMargin, 5, centre)
        e.Graphics.DrawString("GUARANTEED QUALITY AND QUANTITY", Font8Italic, Brushes.Black, CentreMargin, 30, centre)

        e.Graphics.DrawString(Line, Font8, Brushes.Black, 0, 50)

        e.Graphics.DrawString("QTY", Font10Bold, Brushes.Black, 0, 75)
        e.Graphics.DrawString("ITEM", Font10Bold, Brushes.Black, CentreMargin, 75, centre)
        e.Graphics.DrawString("TOTAL", Font10Bold, Brushes.Black, RightMargin, 75, Right)



        'ACTUAL ITEMS
        Dim Height As Integer
        Dim i As Long
        dgvList.AllowUserToAddRows = False
        For row As Integer = 0 To dgvList.RowCount - 1
            Height += 15
            'NEXT LINE PRINTS QUANTITY(S) TO RECEIPT
            e.Graphics.DrawString(dgvList.Rows(row).Cells(1).Value.ToString, Font8, Brushes.Black, 10, 100 + Height)
            'GETS ITEM
            e.Graphics.DrawString(dgvList.Rows(row).Cells(0).Value.ToString, Font8, Brushes.Black, CentreMargin, 100 + Height, centre)
            'GETS TOTAL
            e.Graphics.DrawString(dgvList.Rows(row).Cells(2).Value.ToString, Font8, Brushes.Black, RightMargin, 100 + Height, Right)

        Next
        'LINE AFTER LIST OF ITEMS
        Dim Height2 As Integer
        Height2 = 110 + Height

        Sumprice() 'CALL THE SUB SUMPRICE

        e.Graphics.DrawString(Line, Font8, Brushes.Black, 0, Height2)
        e.Graphics.DrawString("GRAND TOTAL : " & TotalPrice, Font10Bold, Brushes.Black, RightMargin, 15 + Height2, Right)
        e.Graphics.DrawString("THANK YOU ", Font8, Brushes.Black, CentreMargin, 50 + Height2, centre)

    End Sub
    Dim TotalPrice As Long
    Sub Sumprice()
        Dim CountPrice As Long = 0
        For RowItem As Long = 0 To dgvList.RowCount - 1
            CountPrice = CountPrice + Val(dgvList.Rows(RowItem).Cells(2).Value)

        Next
        TotalPrice = CountPrice
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PPD.Document = PD
        PPD.ShowDialog()

    End Sub
End Class
