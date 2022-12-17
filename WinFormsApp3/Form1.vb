Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Dim cn As New SqlConnection("initial catalog=etudiant;data source=DESKTOP-RDOH2QI\SQLEXPRESS;integrated security=true")
    Function connect()
        If cn.State = ConnectionState.Closed Then
            cn.Open()
        End If
        Return True
    End Function
    Function datagrid()

        Dim cmd2 As New SqlCommand("select * from note_E", cn)
        Dim rd As SqlDataReader = cmd2.ExecuteReader
        Dim t As New DataTable
        t.Load(rd)
        rd.Close()
        DataGridView1.DataSource = t
    End Function


    Function Total()
        Dim cmd5 As New SqlCommand("Select count(*) From note_E", cn)
        TextBox5.Text = cmd5.ExecuteScalar.ToString
    End Function
    Function min()
        Dim cmd6 As New SqlCommand("Select min(note) From note_E", cn)
        TextBox6.Text = cmd6.ExecuteScalar.ToString
    End Function

    Function max()
        Dim cmd7 As New SqlCommand("Select max(note) From note_E", cn)
        TextBox7.Text = cmd7.ExecuteScalar.ToString
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        connect()
        Dim cmd As New SqlCommand("Select * from note_E where nom = '" & TextBox1.Text & "'", cn)
        Dim rd As SqlDataReader = cmd.ExecuteReader
        Dim t As New DataTable
        t.Load(rd)
        rd.Close()
        DataGridView1.DataSource = t
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If connect() = True Then
            MessageBox.Show("connexion succes")
        End If
        min()
        max()
        Total()
        datagrid()

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        min()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cmd As New SqlCommand("DELETE FROM note_E WHERE nom = '" & TextBox1.Text & "' OR prenom ='" & TextBox2.Text & "' ", cn)
        cmd.ExecuteNonQuery()
        datagrid()
        connect()
        Total()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim str1 = TextBox3.Text.Replace(",", ".")
        Dim cmd As New SqlCommand("Insert into note_E values('" & TextBox1.Text & "','" & TextBox2.Text & "'," & str1 & ")", cn)
        cmd.ExecuteNonQuery()
        datagrid()
        connect()
        Total()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        Total()
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        max()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim str1 = TextBox3.Text.Replace(",", ".")
        Dim cmd As New SqlCommand("UPDATE note_E SET nom = '" & TextBox1.Text & "', prenom= '" & TextBox2.Text & "', note=" & str1 & " WHERE nom = '" & TextBox1.Text & "' OR prenom = '" & TextBox2.Text & "'", cn)
        cmd.ExecuteNonQuery()
        datagrid()
        connect()
        Total()
    End Sub
End Class
