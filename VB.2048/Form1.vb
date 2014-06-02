Public Class Form1
    Dim BGColor As Color = Color.FromArgb(250, 248, 239)
    Dim WinColor As Color = Color.FromArgb(243, 221, 143)
    Dim GridPos(3, 3) As Point
    Dim TileState(3, 3) As Integer
    Dim TileSize As Size = New Size(102, 102)
    Dim playing As Boolean = False
    Dim bestChanged As Boolean = False
    Dim score As Integer
    Dim best As Single

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call fillGridPos()
        Call setTilesPos()
        Me.BackColor = BGColor
        Grid.Image = New Bitmap("data/gui/grid.png")
        Grid.BackColor = BGColor
        Banner.Image = New Bitmap("data/gui/banner.png")
        Banner.BackColor = BGColor
        ScoreBox.Image = New Bitmap("data/gui/score.png")
        ScoreBox.BackColor = BGColor
        BestBox.Image = New Bitmap("data/gui/best.png")
        BestBox.BackColor = BGColor
        Call clearAllTiles()
        Call loadBestScore()
        Randomize()
    End Sub

    Private Sub fillGridPos()
        GridPos(0, 0) = New Point(Grid.Location.X + 14, Grid.Location.Y + 14)
        GridPos(0, 1) = New Point(Grid.Location.X + 14, Grid.Location.Y + 131)
        GridPos(0, 2) = New Point(Grid.Location.X + 14, Grid.Location.Y + 248)
        GridPos(0, 3) = New Point(Grid.Location.X + 14, Grid.Location.Y + 365)
        GridPos(1, 0) = New Point(Grid.Location.X + 131, Grid.Location.Y + 14)
        GridPos(1, 1) = New Point(Grid.Location.X + 131, Grid.Location.Y + 131)
        GridPos(1, 2) = New Point(Grid.Location.X + 131, Grid.Location.Y + 248)
        GridPos(1, 3) = New Point(Grid.Location.X + 131, Grid.Location.Y + 365)
        GridPos(2, 0) = New Point(Grid.Location.X + 248, Grid.Location.Y + 14)
        GridPos(2, 1) = New Point(Grid.Location.X + 248, Grid.Location.Y + 131)
        GridPos(2, 2) = New Point(Grid.Location.X + 248, Grid.Location.Y + 248)
        GridPos(2, 3) = New Point(Grid.Location.X + 248, Grid.Location.Y + 365)
        GridPos(3, 0) = New Point(Grid.Location.X + 365, Grid.Location.Y + 14)
        GridPos(3, 1) = New Point(Grid.Location.X + 365, Grid.Location.Y + 131)
        GridPos(3, 2) = New Point(Grid.Location.X + 365, Grid.Location.Y + 248)
        GridPos(3, 3) = New Point(Grid.Location.X + 365, Grid.Location.Y + 365)
    End Sub

    Private Sub loadBestScore()
        On Error GoTo CreateNewScore
        Dim scorereader As System.IO.StreamReader = New System.IO.StreamReader("data/score/best.score")
        Dim enc_best As Double = scorereader.ReadToEnd()
        enc_best = enc_best / 8
        enc_best = -enc_best
        enc_best = enc_best - 8.75
        enc_best = enc_best / 3
        best = enc_best * 4
        If best < 0 Then
            GoTo CreateNewScore
        End If
        If best <> CInt(best) Then
            GoTo CreateNewScore
        End If
        BestText.Text = best
        scorereader.Close()
        Exit Sub
CreateNewScore:
        If ErrorToString.Contains("Could not find a part of the path") Then System.IO.Directory.CreateDirectory("data/score/") Else scorereader.Close()
        Dim scorewriter As System.IO.StreamWriter = New System.IO.StreamWriter("data/score/best.score")
        scorewriter.Write(0)
        scorewriter.Close()
        best = 0
    End Sub

    Private Sub saveBestScore()
        Dim scorewriter As System.IO.StreamWriter = New System.IO.StreamWriter("data/score/best.score")
        Dim enc_best As Double = best
        enc_best = enc_best / 4
        enc_best = enc_best * 3
        enc_best = enc_best + 8.75
        enc_best = (-enc_best) * 8
        scorewriter.Write(enc_best)
        scorewriter.Close()
    End Sub

    Private Sub setTilesPos()
        Tile00.Location = GridPos(0, 0)
        Tile01.Location = GridPos(0, 1)
        Tile02.Location = GridPos(0, 2)
        Tile03.Location = GridPos(0, 3)
        Tile10.Location = GridPos(1, 0)
        Tile11.Location = GridPos(1, 1)
        Tile12.Location = GridPos(1, 2)
        Tile13.Location = GridPos(1, 3)
        Tile20.Location = GridPos(2, 0)
        Tile21.Location = GridPos(2, 1)
        Tile22.Location = GridPos(2, 2)
        Tile23.Location = GridPos(2, 3)
        Tile30.Location = GridPos(3, 0)
        Tile31.Location = GridPos(3, 1)
        Tile32.Location = GridPos(3, 2)
        Tile33.Location = GridPos(3, 3)
    End Sub

    Private Sub clearAllTiles()
        Tile00.Image = New Bitmap("data/tiles/tile_0.png")
        Tile01.Image = New Bitmap("data/tiles/tile_0.png")
        Tile02.Image = New Bitmap("data/tiles/tile_0.png")
        Tile03.Image = New Bitmap("data/tiles/tile_0.png")
        Tile10.Image = New Bitmap("data/tiles/tile_0.png")
        Tile11.Image = New Bitmap("data/tiles/tile_0.png")
        Tile12.Image = New Bitmap("data/tiles/tile_0.png")
        Tile13.Image = New Bitmap("data/tiles/tile_0.png")
        Tile20.Image = New Bitmap("data/tiles/tile_0.png")
        Tile21.Image = New Bitmap("data/tiles/tile_0.png")
        Tile22.Image = New Bitmap("data/tiles/tile_0.png")
        Tile23.Image = New Bitmap("data/tiles/tile_0.png")
        Tile30.Image = New Bitmap("data/tiles/tile_0.png")
        Tile31.Image = New Bitmap("data/tiles/tile_0.png")
        Tile32.Image = New Bitmap("data/tiles/tile_0.png")
        Tile33.Image = New Bitmap("data/tiles/tile_0.png")
        For x = 0 To 3
            For y = 0 To 3
                TileState(x, y) = 0
            Next y
        Next x
    End Sub

    Private Function generateTileObject(ByVal TileX, ByVal TileY)
        If TileX = 0 And TileY = 0 Then
            Return Tile00
        End If
        If TileX = 0 And TileY = 1 Then
            Return Tile01
        End If
        If TileX = 0 And TileY = 2 Then
            Return Tile02
        End If
        If TileX = 0 And TileY = 3 Then
            Return Tile03
        End If

        If TileX = 1 And TileY = 0 Then Return Tile10
        If TileX = 1 And TileY = 1 Then Return Tile11
        If TileX = 1 And TileY = 2 Then Return Tile12
        If TileX = 1 And TileY = 3 Then Return Tile13

        If TileX = 2 And TileY = 0 Then
            Return Tile20
        End If
        If TileX = 2 And TileY = 1 Then
            Return Tile21
        End If
        If TileX = 2 And TileY = 2 Then
            Return Tile22
        End If
        If TileX = 2 And TileY = 3 Then
            Return Tile23
        End If

        If TileX = 3 And TileY = 0 Then Return Tile30
        If TileX = 3 And TileY = 1 Then Return Tile31
        If TileX = 3 And TileY = 2 Then Return Tile32
        If TileX = 3 And TileY = 3 Then Return Tile33

        Return generateTileObject(CInt(Rnd() * 3), CInt(Rnd() * 3))
    End Function

    Private Sub NewGame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewGameButton.Click
        If playing = True Then
            If MsgBox("Are you sure you want to start a new game? You will lose the current game.", MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.Yes Then
                playing = False
            Else
                Exit Sub
            End If
        End If
        Banner.Image = New Bitmap("data/gui/banner.png")
        Me.BackColor = BGColor : Banner.BackColor = BGColor
        NewGameButton.BackgroundImage = Nothing
        NewGameButton.Image = New Bitmap("data/gui/newgame.png")
        playing = False
        bestChanged = False
        score = 0
        ScoreText.Text = score
        Call clearAllTiles()
firsttilesgen:
        Dim Tile1 As System.Object = generateTileObject(CInt(Rnd() * 3), CInt(Rnd() * 3))
        Dim Tile2 As System.Object = generateTileObject(CInt(Rnd() * 3), CInt(Rnd() * 3))
        If Tile1 Is Tile2 Then GoTo firsttilesgen
        Dim Tile1Is4 As Boolean = random4()
        Dim TileX As Single = Microsoft.VisualBasic.Mid(Tile1.Name.ToString(), 5, 1)
        Dim TileY As Single = Microsoft.VisualBasic.Mid(Tile1.Name.ToString(), 6, 1)
        If Tile1Is4 = True Then
            Tile1.Image = New Bitmap("data/tiles/tile_4.png")
            TileState(TileX, TileY) = 4
        Else
            Tile1.Image = New Bitmap("data/tiles/tile_2.png")
            TileState(TileX, TileY) = 2
        End If
        Dim Tile2Is4 As Boolean = random4()
        TileX = Microsoft.VisualBasic.Mid(Tile2.Name.ToString(), 5, 1)
        TileY = Microsoft.VisualBasic.Mid(Tile2.Name.ToString(), 6, 1)
        If Tile2Is4 = True Then
            Tile2.Image = New Bitmap("data/tiles/tile_4.png")
            TileState(TileX, TileY) = 4
        Else
            Tile2.Image = New Bitmap("data/tiles/tile_2.png")
            TileState(TileX, TileY) = 2
        End If
        playing = True
    End Sub

    Private Function random4()
        If CInt(Rnd() * 10) = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub generateNewTile()
genstart:
        Dim Tile As System.Object = generateTileObject(CInt(Rnd() * 3), CInt(Rnd() * 3))
        Dim TileIs4 As Boolean = random4()
        Dim TileX As Single = Microsoft.VisualBasic.Mid(Tile.Name.ToString(), 5, 1)
        Dim TileY As Single = Microsoft.VisualBasic.Mid(Tile.Name.ToString(), 6, 1)
        If TileState(TileX, TileY) <> 0 Then
            GoTo genstart
        End If
        If TileIs4 = True Then
            Tile.Image = New Bitmap("data/tiles/tile_4.png")
            TileState(TileX, TileY) = 4
        Else
            Tile.Image = New Bitmap("data/tiles/tile_2.png")
            TileState(TileX, TileY) = 2
        End If
    End Sub

    Private Sub Form1_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Down, Keys.Up, Keys.Left, Keys.Right
                e.IsInputKey = True
        End Select
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If playing = True Then
            If movePossible() Then
                Select Case (e.KeyCode)
                    Case Keys.Left
                        Dim MoveMade As Boolean = False
left:
                        If TileState(0, 0) = 0 Then
                            If TileState(1, 0) <> 0 Or TileState(2, 0) <> 0 Or TileState(3, 0) <> 0 Then
                                Tile00.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                                TileState(0, 0) = TileState(1, 0)
                                Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                                TileState(1, 0) = TileState(2, 0)
                                Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                                TileState(2, 0) = TileState(3, 0)
                                Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 0) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If
                        If TileState(1, 0) = 0 Then
                            If TileState(2, 0) <> 0 Or TileState(3, 0) <> 0 Then
                                Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                                TileState(1, 0) = TileState(2, 0)
                                Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                                TileState(2, 0) = TileState(3, 0)
                                Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 0) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If
                        If TileState(2, 0) = 0 Then
                            If TileState(3, 0) <> 0 Then
                                Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                                TileState(2, 0) = TileState(3, 0)
                                Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 0) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If

                        If TileState(0, 1) = 0 Then
                            If TileState(1, 1) <> 0 Or TileState(2, 1) <> 0 Or TileState(3, 1) <> 0 Then
                                Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                                TileState(0, 1) = TileState(1, 1)
                                Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                                TileState(1, 1) = TileState(2, 1)
                                Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                                TileState(2, 1) = TileState(3, 1)
                                Tile31.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 1) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If
                        If TileState(1, 1) = 0 Then
                            If TileState(2, 1) <> 0 Or TileState(3, 1) <> 0 Then
                                Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                                TileState(1, 1) = TileState(2, 1)
                                Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                                TileState(2, 1) = TileState(3, 1)
                                Tile31.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 1) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If
                        If TileState(2, 1) = 0 Then
                            If TileState(3, 1) <> 0 Then
                                Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                                TileState(2, 1) = TileState(3, 1)
                                Tile31.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 1) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If

                        If TileState(0, 2) = 0 Then
                            If TileState(1, 2) <> 0 Or TileState(2, 2) <> 0 Or TileState(3, 2) <> 0 Then
                                Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                                TileState(0, 2) = TileState(1, 2)
                                Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                                TileState(1, 2) = TileState(2, 2)
                                Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                                TileState(2, 2) = TileState(3, 2)
                                Tile32.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 2) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If
                        If TileState(1, 2) = 0 Then
                            If TileState(2, 2) <> 0 Or TileState(3, 2) <> 0 Then
                                Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                                TileState(1, 2) = TileState(2, 2)
                                Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                                TileState(2, 2) = TileState(3, 2)
                                Tile32.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 2) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If
                        If TileState(2, 2) = 0 Then
                            If TileState(3, 2) <> 0 Then
                                Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                                TileState(2, 2) = TileState(3, 2)
                                Tile32.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 2) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If

                        If TileState(0, 3) = 0 Then
                            If TileState(1, 3) <> 0 Or TileState(2, 3) <> 0 Or TileState(3, 3) <> 0 Then
                                Tile03.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                                TileState(0, 3) = TileState(1, 3)
                                Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                                TileState(1, 3) = TileState(2, 3)
                                Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                                TileState(2, 3) = TileState(3, 3)
                                Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 3) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If
                        If TileState(1, 3) = 0 Then
                            If TileState(2, 3) <> 0 Or TileState(3, 3) <> 0 Then
                                Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                                TileState(1, 3) = TileState(2, 3)
                                Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                                TileState(2, 3) = TileState(3, 3)
                                Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 3) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If
                        If TileState(2, 3) = 0 Then
                            If TileState(3, 3) <> 0 Then
                                Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                                TileState(2, 3) = TileState(3, 3)
                                Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 3) = 0
                                MoveMade = True
                                GoTo left
                            End If
                        End If

                        If MoveMade = True Or mergeTilesLeft() = True Then
                            Call generateNewTile()
                        End If
                    Case Keys.Up
                        Dim MoveMade As Boolean = False
up:
                        If TileState(0, 0) = 0 Then
                            If TileState(0, 1) <> 0 Or TileState(0, 2) <> 0 Or TileState(0, 3) <> 0 Then
                                Tile00.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                                TileState(0, 0) = TileState(0, 1)
                                Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                                TileState(0, 1) = TileState(0, 2)
                                Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                                TileState(0, 2) = TileState(0, 3)
                                Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If
                        If TileState(0, 1) = 0 Then
                            If TileState(0, 2) <> 0 Or TileState(0, 3) <> 0 Then
                                Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                                TileState(0, 1) = TileState(0, 2)
                                Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                                TileState(0, 2) = TileState(0, 3)
                                Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If
                        If TileState(0, 2) = 0 Then
                            If TileState(0, 3) <> 0 Then
                                Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                                TileState(0, 2) = TileState(0, 3)
                                Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If

                        If TileState(1, 0) = 0 Then
                            If TileState(1, 1) <> 0 Or TileState(1, 2) <> 0 Or TileState(1, 3) <> 0 Then
                                Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                                TileState(1, 0) = TileState(1, 1)
                                Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                                TileState(1, 1) = TileState(1, 2)
                                Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                                TileState(1, 2) = TileState(1, 3)
                                Tile13.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(1, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If
                        If TileState(1, 1) = 0 Then
                            If TileState(1, 2) <> 0 Or TileState(1, 3) <> 0 Then
                                Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                                TileState(1, 1) = TileState(1, 2)
                                Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                                TileState(1, 2) = TileState(1, 3)
                                Tile13.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(1, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If
                        If TileState(1, 2) = 0 Then
                            If TileState(1, 3) <> 0 Then
                                Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                                TileState(1, 2) = TileState(1, 3)
                                Tile13.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(1, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If

                        If TileState(2, 0) = 0 Then
                            If TileState(2, 1) <> 0 Or TileState(2, 2) <> 0 Or TileState(2, 3) <> 0 Then
                                Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                                TileState(2, 0) = TileState(2, 1)
                                Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                                TileState(2, 1) = TileState(2, 2)
                                Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                                TileState(2, 2) = TileState(2, 3)
                                Tile23.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(2, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If
                        If TileState(2, 1) = 0 Then
                            If TileState(2, 2) <> 0 Or TileState(2, 3) <> 0 Then
                                Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                                TileState(2, 1) = TileState(2, 2)
                                Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                                TileState(2, 2) = TileState(2, 3)
                                Tile23.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(2, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If
                        If TileState(2, 2) = 0 Then
                            If TileState(2, 3) <> 0 Then
                                Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                                TileState(2, 2) = TileState(2, 3)
                                Tile23.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(2, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If

                        If TileState(3, 0) = 0 Then
                            If TileState(3, 1) <> 0 Or TileState(3, 2) <> 0 Or TileState(3, 3) <> 0 Then
                                Tile30.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                                TileState(3, 0) = TileState(3, 1)
                                Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                                TileState(3, 1) = TileState(3, 2)
                                Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                                TileState(3, 2) = TileState(3, 3)
                                Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If
                        If TileState(3, 1) = 0 Then
                            If TileState(3, 2) <> 0 Or TileState(3, 3) <> 0 Then
                                Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                                TileState(3, 1) = TileState(3, 2)
                                Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                                TileState(3, 2) = TileState(3, 3)
                                Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If
                        If TileState(3, 2) = 0 Then
                            If TileState(3, 3) <> 0 Then
                                Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                                TileState(3, 2) = TileState(3, 3)
                                Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 3) = 0
                                MoveMade = True
                                GoTo up
                            End If
                        End If

                        If MoveMade = True Or mergeTilesUp() = True Then
                            Call generateNewTile()
                        End If
                    Case Keys.Right
                        Dim MoveMade As Boolean = False
right:
                        If TileState(3, 0) = 0 Then
                            If TileState(2, 0) <> 0 Or TileState(1, 0) <> 0 Or TileState(0, 0) <> 0 Then
                                Tile30.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                                TileState(3, 0) = TileState(2, 0)
                                Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                                TileState(2, 0) = TileState(1, 0)
                                Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                                TileState(1, 0) = TileState(0, 0)
                                Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 0) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If
                        If TileState(2, 0) = 0 Then
                            If TileState(1, 0) <> 0 Or TileState(0, 0) <> 0 Then
                                Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                                TileState(2, 0) = TileState(1, 0)
                                Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                                TileState(1, 0) = TileState(0, 0)
                                Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 0) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If
                        If TileState(1, 0) = 0 Then
                            If TileState(0, 0) <> 0 Then
                                Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                                TileState(1, 0) = TileState(0, 0)
                                Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 0) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If

                        If TileState(3, 1) = 0 Then
                            If TileState(2, 1) <> 0 Or TileState(1, 1) <> 0 Or TileState(0, 1) <> 0 Then
                                Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                                TileState(3, 1) = TileState(2, 1)
                                Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                                TileState(2, 1) = TileState(1, 1)
                                Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                                TileState(1, 1) = TileState(0, 1)
                                Tile01.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 1) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If
                        If TileState(2, 1) = 0 Then
                            If TileState(1, 1) <> 0 Or TileState(0, 1) <> 0 Then
                                Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                                TileState(2, 1) = TileState(1, 1)
                                Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                                TileState(1, 1) = TileState(0, 1)
                                Tile01.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 1) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If
                        If TileState(1, 1) = 0 Then
                            If TileState(0, 1) <> 0 Then
                                Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                                TileState(1, 1) = TileState(0, 1)
                                Tile01.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 1) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If

                        If TileState(3, 2) = 0 Then
                            If TileState(2, 2) <> 0 Or TileState(1, 2) <> 0 Or TileState(0, 2) <> 0 Then
                                Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                                TileState(3, 2) = TileState(2, 2)
                                Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                                TileState(2, 2) = TileState(1, 2)
                                Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                                TileState(1, 2) = TileState(0, 2)
                                Tile02.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 2) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If
                        If TileState(2, 2) = 0 Then
                            If TileState(1, 2) <> 0 Or TileState(0, 2) <> 0 Then
                                Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                                TileState(2, 2) = TileState(1, 2)
                                Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                                TileState(1, 2) = TileState(0, 2)
                                Tile02.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 2) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If
                        If TileState(1, 2) = 0 Then
                            If TileState(0, 2) <> 0 Then
                                Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                                TileState(1, 2) = TileState(0, 2)
                                Tile02.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 2) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If

                        If TileState(3, 3) = 0 Then
                            If TileState(2, 3) <> 0 Or TileState(1, 3) <> 0 Or TileState(0, 3) <> 0 Then
                                Tile33.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                                TileState(3, 3) = TileState(2, 3)
                                Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                                TileState(2, 3) = TileState(1, 3)
                                Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                                TileState(1, 3) = TileState(0, 3)
                                Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 3) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If
                        If TileState(2, 3) = 0 Then
                            If TileState(1, 3) <> 0 Or TileState(0, 3) <> 0 Then
                                Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                                TileState(2, 3) = TileState(1, 3)
                                Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                                TileState(1, 3) = TileState(0, 3)
                                Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 3) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If
                        If TileState(1, 3) = 0 Then
                            If TileState(0, 3) <> 0 Then
                                Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                                TileState(1, 3) = TileState(0, 3)
                                Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 3) = 0
                                MoveMade = True
                                GoTo right
                            End If
                        End If

                        If MoveMade = True Or mergeTilesRight() = True Then
                            Call generateNewTile()
                        End If
                    Case Keys.Down
                        Dim MoveMade As Boolean = False
down:
                        If TileState(0, 3) = 0 Then
                            If TileState(0, 2) <> 0 Or TileState(0, 1) <> 0 Or TileState(0, 0) <> 0 Then
                                Tile03.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                                TileState(0, 3) = TileState(0, 2)
                                Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                                TileState(0, 2) = TileState(0, 1)
                                Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                                TileState(0, 1) = TileState(0, 0)
                                Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If
                        If TileState(0, 2) = 0 Then
                            If TileState(0, 1) <> 0 Or TileState(0, 0) <> 0 Then
                                Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                                TileState(0, 2) = TileState(0, 1)
                                Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                                TileState(0, 1) = TileState(0, 0)
                                Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If
                        If TileState(0, 1) = 0 Then
                            If TileState(0, 0) <> 0 Then
                                Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                                TileState(0, 1) = TileState(0, 0)
                                Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(0, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If

                        If TileState(1, 3) = 0 Then
                            If TileState(1, 2) <> 0 Or TileState(1, 1) <> 0 Or TileState(1, 0) <> 0 Then
                                Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                                TileState(1, 3) = TileState(1, 2)
                                Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                                TileState(1, 2) = TileState(1, 1)
                                Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                                TileState(1, 1) = TileState(1, 0)
                                Tile10.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(1, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If
                        If TileState(1, 2) = 0 Then
                            If TileState(1, 1) <> 0 Or TileState(1, 0) <> 0 Then
                                Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                                TileState(1, 2) = TileState(1, 1)
                                Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                                TileState(1, 1) = TileState(1, 0)
                                Tile10.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(1, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If
                        If TileState(1, 1) = 0 Then
                            If TileState(1, 0) <> 0 Then
                                Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                                TileState(1, 1) = TileState(1, 0)
                                Tile10.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(1, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If

                        If TileState(2, 3) = 0 Then
                            If TileState(2, 2) <> 0 Or TileState(2, 1) <> 0 Or TileState(2, 0) <> 0 Then
                                Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                                TileState(2, 3) = TileState(2, 2)
                                Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                                TileState(2, 2) = TileState(2, 1)
                                Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                                TileState(2, 1) = TileState(2, 0)
                                Tile20.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(2, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If
                        If TileState(2, 2) = 0 Then
                            If TileState(2, 1) <> 0 Or TileState(2, 0) <> 0 Then
                                Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                                TileState(2, 2) = TileState(2, 1)
                                Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                                TileState(2, 1) = TileState(2, 0)
                                Tile20.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(2, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If
                        If TileState(2, 1) = 0 Then
                            If TileState(2, 0) <> 0 Then
                                Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                                TileState(2, 1) = TileState(2, 0)
                                Tile20.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(2, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If

                        If TileState(3, 3) = 0 Then
                            If TileState(3, 2) <> 0 Or TileState(3, 1) <> 0 Or TileState(3, 0) <> 0 Then
                                Tile33.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                                TileState(3, 3) = TileState(3, 2)
                                Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                                TileState(3, 2) = TileState(3, 1)
                                Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                                TileState(3, 1) = TileState(3, 0)
                                Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If
                        If TileState(3, 2) = 0 Then
                            If TileState(3, 1) <> 0 Or TileState(3, 0) <> 0 Then
                                Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                                TileState(3, 2) = TileState(3, 1)
                                Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                                TileState(3, 1) = TileState(3, 0)
                                Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If
                        If TileState(3, 1) = 0 Then
                            If TileState(3, 0) <> 0 Then
                                Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                                TileState(3, 1) = TileState(3, 0)
                                Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                                TileState(3, 0) = 0
                                MoveMade = True
                                GoTo down
                            End If
                        End If

                        If MoveMade = True Or mergeTilesDown() = True Then
                            Call generateNewTile()
                        End If
                End Select
                If gameWon() Then
                    Banner.Image = New Bitmap("data/gui/youwin.png")
                    Me.BackColor = WinColor : Banner.BackColor = WinColor
                    NewGameButton.BackgroundImage = New Bitmap("data/gui/tryagain.png")
                    NewGameButton.Image = Nothing
                    playing = False
                    If bestChanged Then
                        saveBestScore()
                    End If
                End If
            Else
                Banner.Image = New Bitmap("data/gui/gameover.png")
                NewGameButton.BackgroundImage = New Bitmap("data/gui/tryagain.png")
                NewGameButton.Image = Nothing
                playing = False
                If bestChanged Then
                    saveBestScore()
                End If
            End If
        End If
    End Sub

    Private Function mergeTilesLeft()
        Dim merged As Boolean = False
        Dim scoreAddition As Integer

        For y = 0 To 3
            For x = 0 To 2
                If TileState(x, y) = TileState(x + 1, y) And TileState(x, y) <> 0 Then
                    TileState(x, y) = TileState(x, y) + TileState(x + 1, y)
                    scoreAddition = scoreAddition + TileState(x, y)
                    TileState(x + 1, y) = 0
                    Dim mergeTileXY As System.Object = generateTileObject(x, y)
                    Dim mergeTileXplus1Y As System.Object = generateTileObject(x + 1, y)
                    mergeTileXY.Image = New Bitmap("data/tiles/tile_" & TileState(x, y) & ".png")
                    mergeTileXplus1Y.Image = New Bitmap("data/tiles/tile_0.png")
                    merged = True
                End If
            Next x
        Next y

        If merged = True Then
check:
            If TileState(0, 0) = 0 Then
                If TileState(1, 0) <> 0 Or TileState(2, 0) <> 0 Or TileState(3, 0) <> 0 Then
                    Tile00.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                    TileState(0, 0) = TileState(1, 0)
                    Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                    TileState(1, 0) = TileState(2, 0)
                    Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                    TileState(2, 0) = TileState(3, 0)
                    Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 0) = 0 Then
                If TileState(2, 0) <> 0 Or TileState(3, 0) <> 0 Then
                    Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                    TileState(1, 0) = TileState(2, 0)
                    Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                    TileState(2, 0) = TileState(3, 0)
                    Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 0) = 0 Then
                If TileState(3, 0) <> 0 Then
                    Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                    TileState(2, 0) = TileState(3, 0)
                    Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 0) = 0
                    GoTo check
                End If
            End If

            If TileState(0, 1) = 0 Then
                If TileState(1, 1) <> 0 Or TileState(2, 1) <> 0 Or TileState(3, 1) <> 0 Then
                    Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                    TileState(0, 1) = TileState(1, 1)
                    Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                    TileState(1, 1) = TileState(2, 1)
                    Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                    TileState(2, 1) = TileState(3, 1)
                    Tile31.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 1) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 1) = 0 Then
                If TileState(2, 1) <> 0 Or TileState(3, 1) <> 0 Then
                    Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                    TileState(1, 1) = TileState(2, 1)
                    Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                    TileState(2, 1) = TileState(3, 1)
                    Tile31.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 1) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 1) = 0 Then
                If TileState(3, 1) <> 0 Then
                    Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                    TileState(2, 1) = TileState(3, 1)
                    Tile31.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 1) = 0
                    GoTo check
                End If
            End If

            If TileState(0, 2) = 0 Then
                If TileState(1, 2) <> 0 Or TileState(2, 2) <> 0 Or TileState(3, 2) <> 0 Then
                    Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                    TileState(0, 2) = TileState(1, 2)
                    Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                    TileState(1, 2) = TileState(2, 2)
                    Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                    TileState(2, 2) = TileState(3, 2)
                    Tile32.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 2) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 2) = 0 Then
                If TileState(2, 2) <> 0 Or TileState(3, 2) <> 0 Then
                    Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                    TileState(1, 2) = TileState(2, 2)
                    Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                    TileState(2, 2) = TileState(3, 2)
                    Tile32.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 2) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 2) = 0 Then
                If TileState(3, 2) <> 0 Then
                    Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                    TileState(2, 2) = TileState(3, 2)
                    Tile32.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 2) = 0
                    GoTo check
                End If
            End If

            If TileState(0, 3) = 0 Then
                If TileState(1, 3) <> 0 Or TileState(2, 3) <> 0 Or TileState(3, 3) <> 0 Then
                    Tile03.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                    TileState(0, 3) = TileState(1, 3)
                    Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                    TileState(1, 3) = TileState(2, 3)
                    Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                    TileState(2, 3) = TileState(3, 3)
                    Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 3) = 0 Then
                If TileState(2, 3) <> 0 Or TileState(3, 3) <> 0 Then
                    Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                    TileState(1, 3) = TileState(2, 3)
                    Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                    TileState(2, 3) = TileState(3, 3)
                    Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 3) = 0 Then
                If TileState(3, 3) <> 0 Then
                    Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                    TileState(2, 3) = TileState(3, 3)
                    Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 3) = 0
                    GoTo check
                End If
            End If
        End If

        Call scoreAdd(scoreAddition)
        Return merged
    End Function

    Private Function mergeTilesRight()
        Dim merged As Boolean = False
        Dim scoreAddition As Integer

        For y = 0 To 3
            For x_inverted = 1 To 3
                Dim x As Single = 4 - x_inverted
                If TileState(x, y) = TileState(x - 1, y) And TileState(x, y) <> 0 Then
                    TileState(x, y) = TileState(x, y) + TileState(x - 1, y)
                    scoreAddition = scoreAddition + TileState(x, y)
                    TileState(x - 1, y) = 0
                    Dim mergeTileXY As System.Object = generateTileObject(x, y)
                    Dim mergeTileXminus1Y As System.Object = generateTileObject(x - 1, y)
                    mergeTileXY.Image = New Bitmap("data/tiles/tile_" & TileState(x, y) & ".png")
                    mergeTileXminus1Y.Image = New Bitmap("data/tiles/tile_0.png")
                    merged = True
                End If
            Next x_inverted
        Next y

        If merged = True Then
check:
            If TileState(3, 0) = 0 Then
                If TileState(2, 0) <> 0 Or TileState(1, 0) <> 0 Or TileState(0, 0) <> 0 Then
                    Tile30.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                    TileState(3, 0) = TileState(2, 0)
                    Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                    TileState(2, 0) = TileState(1, 0)
                    Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                    TileState(1, 0) = TileState(0, 0)
                    Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 0) = 0 Then
                If TileState(1, 0) <> 0 Or TileState(0, 0) <> 0 Then
                    Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                    TileState(2, 0) = TileState(1, 0)
                    Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                    TileState(1, 0) = TileState(0, 0)
                    Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 0) = 0 Then
                If TileState(0, 0) <> 0 Then
                    Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                    TileState(1, 0) = TileState(0, 0)
                    Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(3, 1) = 0 Then
                If TileState(2, 1) <> 0 Or TileState(1, 1) <> 0 Or TileState(0, 1) <> 0 Then
                    Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                    TileState(3, 1) = TileState(2, 1)
                    Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                    TileState(2, 1) = TileState(1, 1)
                    Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                    TileState(1, 1) = TileState(0, 1)
                    Tile01.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 1) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 1) = 0 Then
                If TileState(1, 1) <> 0 Or TileState(0, 1) <> 0 Then
                    Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                    TileState(2, 1) = TileState(1, 1)
                    Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                    TileState(1, 1) = TileState(0, 1)
                    Tile01.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 1) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 1) = 0 Then
                If TileState(0, 1) <> 0 Then
                    Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                    TileState(1, 1) = TileState(0, 1)
                    Tile01.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 1) = 0
                    GoTo check
                End If
            End If

            If TileState(3, 2) = 0 Then
                If TileState(2, 2) <> 0 Or TileState(1, 2) <> 0 Or TileState(0, 2) <> 0 Then
                    Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                    TileState(3, 2) = TileState(2, 2)
                    Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                    TileState(2, 2) = TileState(1, 2)
                    Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                    TileState(1, 2) = TileState(0, 2)
                    Tile02.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 2) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 2) = 0 Then
                If TileState(1, 2) <> 0 Or TileState(0, 2) <> 0 Then
                    Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                    TileState(2, 2) = TileState(1, 2)
                    Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                    TileState(1, 2) = TileState(0, 2)
                    Tile02.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 2) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 2) = 0 Then
                If TileState(0, 2) <> 0 Then
                    Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                    TileState(1, 2) = TileState(0, 2)
                    Tile02.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 2) = 0
                    GoTo check
                End If
            End If

            If TileState(3, 3) = 0 Then
                If TileState(2, 3) <> 0 Or TileState(1, 3) <> 0 Or TileState(0, 3) <> 0 Then
                    Tile33.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                    TileState(3, 3) = TileState(2, 3)
                    Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                    TileState(2, 3) = TileState(1, 3)
                    Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                    TileState(1, 3) = TileState(0, 3)
                    Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 3) = 0 Then
                If TileState(1, 3) <> 0 Or TileState(0, 3) <> 0 Then
                    Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                    TileState(2, 3) = TileState(1, 3)
                    Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                    TileState(1, 3) = TileState(0, 3)
                    Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 3) = 0 Then
                If TileState(0, 3) <> 0 Then
                    Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                    TileState(1, 3) = TileState(0, 3)
                    Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 3) = 0
                    GoTo check
                End If
            End If
        End If

        Call scoreAdd(scoreAddition)
        Return merged
    End Function

    Private Function mergeTilesUp()
        Dim merged As Boolean = False
        Dim scoreAddition As Integer

        For x = 0 To 3
            For y = 0 To 2
                If TileState(x, y) = TileState(x, y + 1) And TileState(x, y) <> 0 Then
                    TileState(x, y) = TileState(x, y) + TileState(x, y + 1)
                    scoreAddition = scoreAddition + TileState(x, y)
                    TileState(x, y + 1) = 0
                    Dim mergeTileXY As System.Object = generateTileObject(x, y)
                    Dim mergeTileXYplus1 As System.Object = generateTileObject(x, y + 1)
                    mergeTileXY.Image = New Bitmap("data/tiles/tile_" & TileState(x, y) & ".png")
                    mergeTileXYplus1.Image = New Bitmap("data/tiles/tile_0.png")
                    merged = True
                End If
            Next y
        Next x

        If merged = True Then
check:
            If TileState(0, 0) = 0 Then
                If TileState(0, 1) <> 0 Or TileState(0, 2) <> 0 Or TileState(0, 3) <> 0 Then
                    Tile00.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                    TileState(0, 0) = TileState(0, 1)
                    Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                    TileState(0, 1) = TileState(0, 2)
                    Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                    TileState(0, 2) = TileState(0, 3)
                    Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(0, 1) = 0 Then
                If TileState(0, 2) <> 0 Or TileState(0, 3) <> 0 Then
                    Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                    TileState(0, 1) = TileState(0, 2)
                    Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                    TileState(0, 2) = TileState(0, 3)
                    Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(0, 2) = 0 Then
                If TileState(0, 3) <> 0 Then
                    Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(0, 3) & ".png")
                    TileState(0, 2) = TileState(0, 3)
                    Tile03.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 3) = 0
                    GoTo check
                End If
            End If

            If TileState(1, 0) = 0 Then
                If TileState(1, 1) <> 0 Or TileState(1, 2) <> 0 Or TileState(1, 3) <> 0 Then
                    Tile10.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                    TileState(1, 0) = TileState(1, 1)
                    Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                    TileState(1, 1) = TileState(1, 2)
                    Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                    TileState(1, 2) = TileState(1, 3)
                    Tile13.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(1, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 1) = 0 Then
                If TileState(1, 2) <> 0 Or TileState(1, 3) <> 0 Then
                    Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                    TileState(1, 1) = TileState(1, 2)
                    Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                    TileState(1, 2) = TileState(1, 3)
                    Tile13.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(1, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 2) = 0 Then
                If TileState(1, 3) <> 0 Then
                    Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(1, 3) & ".png")
                    TileState(1, 2) = TileState(1, 3)
                    Tile13.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(1, 3) = 0
                    GoTo check
                End If
            End If

            If TileState(2, 0) = 0 Then
                If TileState(2, 1) <> 0 Or TileState(2, 2) <> 0 Or TileState(2, 3) <> 0 Then
                    Tile20.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                    TileState(2, 0) = TileState(2, 1)
                    Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                    TileState(2, 1) = TileState(2, 2)
                    Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                    TileState(2, 2) = TileState(2, 3)
                    Tile23.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(2, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 1) = 0 Then
                If TileState(2, 2) <> 0 Or TileState(2, 3) <> 0 Then
                    Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                    TileState(2, 1) = TileState(2, 2)
                    Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                    TileState(2, 2) = TileState(2, 3)
                    Tile23.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(2, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 2) = 0 Then
                If TileState(2, 3) <> 0 Then
                    Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(2, 3) & ".png")
                    TileState(2, 2) = TileState(2, 3)
                    Tile23.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(2, 3) = 0
                    GoTo check
                End If
            End If

            If TileState(3, 0) = 0 Then
                If TileState(3, 1) <> 0 Or TileState(3, 2) <> 0 Or TileState(3, 3) <> 0 Then
                    Tile30.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                    TileState(3, 0) = TileState(3, 1)
                    Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                    TileState(3, 1) = TileState(3, 2)
                    Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                    TileState(3, 2) = TileState(3, 3)
                    Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(3, 1) = 0 Then
                If TileState(3, 2) <> 0 Or TileState(3, 3) <> 0 Then
                    Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                    TileState(3, 1) = TileState(3, 2)
                    Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                    TileState(3, 2) = TileState(3, 3)
                    Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 3) = 0
                    GoTo check
                End If
            End If
            If TileState(3, 2) = 0 Then
                If TileState(3, 3) <> 0 Then
                    Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(3, 3) & ".png")
                    TileState(3, 2) = TileState(3, 3)
                    Tile33.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 3) = 0
                    GoTo check
                End If
            End If
        End If

        Call scoreAdd(scoreAddition)
        Return merged
    End Function

    Private Function mergeTilesDown() 'not work
        Dim merged As Boolean = False
        Dim scoreAddition As Integer

        For x = 0 To 3
            For y_inverted = 1 To 3
                Dim y As Single = 4 - y_inverted
                If TileState(x, y) = TileState(x, y - 1) And TileState(x, y) <> 0 Then
                    TileState(x, y) = TileState(x, y) + TileState(x, y - 1)
                    scoreAddition = scoreAddition + TileState(x, y)
                    TileState(x, y - 1) = 0
                    Dim mergeTileXY As System.Object = generateTileObject(x, y)
                    Dim mergeTileXYminus1 As System.Object = generateTileObject(x, y - 1)
                    mergeTileXY.Image = New Bitmap("data/tiles/tile_" & TileState(x, y) & ".png")
                    mergeTileXYminus1.Image = New Bitmap("data/tiles/tile_0.png")
                    merged = True
                End If
            Next y_inverted
        Next x

        If merged = True Then
check:
            If TileState(0, 3) = 0 Then
                If TileState(0, 2) <> 0 Or TileState(0, 1) <> 0 Or TileState(0, 0) <> 0 Then
                    Tile03.Image = New Bitmap("data/tiles/tile_" & TileState(0, 2) & ".png")
                    TileState(0, 3) = TileState(0, 2)
                    Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                    TileState(0, 2) = TileState(0, 1)
                    Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                    TileState(0, 1) = TileState(0, 0)
                    Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(0, 2) = 0 Then
                If TileState(0, 1) <> 0 Or TileState(0, 0) <> 0 Then
                    Tile02.Image = New Bitmap("data/tiles/tile_" & TileState(0, 1) & ".png")
                    TileState(0, 2) = TileState(0, 1)
                    Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                    TileState(0, 1) = TileState(0, 0)
                    Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(0, 1) = 0 Then
                If TileState(0, 0) <> 0 Then
                    Tile01.Image = New Bitmap("data/tiles/tile_" & TileState(0, 0) & ".png")
                    TileState(0, 1) = TileState(0, 0)
                    Tile00.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(0, 0) = 0
                    GoTo check
                End If
            End If

            If TileState(1, 3) = 0 Then
                If TileState(1, 2) <> 0 Or TileState(1, 1) <> 0 Or TileState(1, 0) <> 0 Then
                    Tile13.Image = New Bitmap("data/tiles/tile_" & TileState(1, 2) & ".png")
                    TileState(1, 3) = TileState(1, 2)
                    Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                    TileState(1, 2) = TileState(1, 1)
                    Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                    TileState(1, 1) = TileState(1, 0)
                    Tile10.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(1, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 2) = 0 Then
                If TileState(1, 1) <> 0 Or TileState(1, 0) <> 0 Then
                    Tile12.Image = New Bitmap("data/tiles/tile_" & TileState(1, 1) & ".png")
                    TileState(1, 2) = TileState(1, 1)
                    Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                    TileState(1, 1) = TileState(1, 0)
                    Tile10.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(1, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(1, 1) = 0 Then
                If TileState(1, 0) <> 0 Then
                    Tile11.Image = New Bitmap("data/tiles/tile_" & TileState(1, 0) & ".png")
                    TileState(1, 1) = TileState(1, 0)
                    Tile10.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(1, 0) = 0
                    GoTo check
                End If
            End If

            If TileState(2, 3) = 0 Then
                If TileState(2, 2) <> 0 Or TileState(2, 1) <> 0 Or TileState(2, 0) <> 0 Then
                    Tile23.Image = New Bitmap("data/tiles/tile_" & TileState(2, 2) & ".png")
                    TileState(2, 3) = TileState(2, 2)
                    Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                    TileState(2, 2) = TileState(2, 1)
                    Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                    TileState(2, 1) = TileState(2, 0)
                    Tile20.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(2, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(2, 2) = 0 Then
                If TileState(2, 1) <> 0 Or TileState(2, 0) <> 0 Then
                    Tile22.Image = New Bitmap("data/tiles/tile_" & TileState(2, 1) & ".png")
                    TileState(2, 2) = TileState(2, 1)
                    Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                    TileState(2, 1) = TileState(2, 0)
                    Tile20.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(2, 0) = 0

                    GoTo check
                End If
            End If
            If TileState(2, 1) = 0 Then
                If TileState(2, 0) <> 0 Then
                    Tile21.Image = New Bitmap("data/tiles/tile_" & TileState(2, 0) & ".png")
                    TileState(2, 1) = TileState(2, 0)
                    Tile20.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(2, 0) = 0
                    GoTo check
                End If
            End If

            If TileState(3, 3) = 0 Then
                If TileState(3, 2) <> 0 Or TileState(3, 1) <> 0 Or TileState(3, 0) <> 0 Then
                    Tile33.Image = New Bitmap("data/tiles/tile_" & TileState(3, 2) & ".png")
                    TileState(3, 3) = TileState(3, 2)
                    Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                    TileState(3, 2) = TileState(3, 1)
                    Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                    TileState(3, 1) = TileState(3, 0)
                    Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(3, 2) = 0 Then
                If TileState(3, 1) <> 0 Or TileState(3, 0) <> 0 Then
                    Tile32.Image = New Bitmap("data/tiles/tile_" & TileState(3, 1) & ".png")
                    TileState(3, 2) = TileState(3, 1)
                    Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                    TileState(3, 1) = TileState(3, 0)
                    Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 0) = 0
                    GoTo check
                End If
            End If
            If TileState(3, 1) = 0 Then
                If TileState(3, 0) <> 0 Then
                    Tile31.Image = New Bitmap("data/tiles/tile_" & TileState(3, 0) & ".png")
                    TileState(3, 1) = TileState(3, 0)
                    Tile30.Image = New Bitmap("data/tiles/tile_0.png")
                    TileState(3, 0) = 0
                    GoTo check
                End If
            End If
        End If

        Call scoreAdd(scoreAddition)
        Return merged
    End Function

    Private Function movePossible()
        If TileState(0, 0) = 0 Then
            If TileState(1, 0) <> 0 Or TileState(2, 0) <> 0 Or TileState(3, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 0) = 0 Then
            If TileState(2, 0) <> 0 Or TileState(3, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 0) = 0 Then
            If TileState(3, 0) <> 0 Then
                Return True
            End If
        End If

        If TileState(0, 1) = 0 Then
            If TileState(1, 1) <> 0 Or TileState(2, 1) <> 0 Or TileState(3, 1) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 1) = 0 Then
            If TileState(2, 1) <> 0 Or TileState(3, 1) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 1) = 0 Then
            If TileState(3, 1) <> 0 Then
                Return True
            End If
        End If

        If TileState(0, 2) = 0 Then
            If TileState(1, 2) <> 0 Or TileState(2, 2) <> 0 Or TileState(3, 2) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 2) = 0 Then
            If TileState(2, 2) <> 0 Or TileState(3, 2) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 2) = 0 Then
            If TileState(3, 2) <> 0 Then
                Return True
            End If
        End If

        If TileState(0, 3) = 0 Then
            If TileState(1, 3) <> 0 Or TileState(2, 3) <> 0 Or TileState(3, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 3) = 0 Then
            If TileState(2, 3) <> 0 Or TileState(3, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 3) = 0 Then
            If TileState(3, 3) <> 0 Then
                Return True
            End If
        End If


        If TileState(0, 0) = 0 Then
            If TileState(0, 1) <> 0 Or TileState(0, 2) <> 0 Or TileState(0, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(0, 1) = 0 Then
            If TileState(0, 2) <> 0 Or TileState(0, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(0, 2) = 0 Then
            If TileState(0, 3) <> 0 Then
                Return True
            End If
        End If

        If TileState(1, 0) = 0 Then
            If TileState(1, 1) <> 0 Or TileState(1, 2) <> 0 Or TileState(1, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 1) = 0 Then
            If TileState(1, 2) <> 0 Or TileState(1, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 2) = 0 Then
            If TileState(1, 3) <> 0 Then
                Return True
            End If
        End If

        If TileState(2, 0) = 0 Then
            If TileState(2, 1) <> 0 Or TileState(2, 2) <> 0 Or TileState(2, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 1) = 0 Then
            If TileState(2, 2) <> 0 Or TileState(2, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 2) = 0 Then
            If TileState(2, 3) <> 0 Then
                Return True
            End If
        End If

        If TileState(3, 0) = 0 Then
            If TileState(3, 1) <> 0 Or TileState(3, 2) <> 0 Or TileState(3, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(3, 1) = 0 Then
            If TileState(3, 2) <> 0 Or TileState(3, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(3, 2) = 0 Then
            If TileState(3, 3) <> 0 Then
                Return True
            End If
        End If


        If TileState(3, 0) = 0 Then
            If TileState(2, 0) <> 0 Or TileState(1, 0) <> 0 Or TileState(0, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 0) = 0 Then
            If TileState(1, 0) <> 0 Or TileState(0, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 0) = 0 Then
            If TileState(0, 0) <> 0 Then
                Return True
            End If
        End If

        If TileState(3, 1) = 0 Then
            If TileState(2, 1) <> 0 Or TileState(1, 1) <> 0 Or TileState(0, 1) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 1) = 0 Then
            If TileState(1, 1) <> 0 Or TileState(0, 1) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 1) = 0 Then
            If TileState(0, 1) <> 0 Then
                Return True
            End If
        End If

        If TileState(3, 2) = 0 Then
            If TileState(2, 2) <> 0 Or TileState(1, 2) <> 0 Or TileState(0, 2) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 2) = 0 Then
            If TileState(1, 2) <> 0 Or TileState(0, 2) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 2) = 0 Then
            If TileState(0, 2) <> 0 Then
                Return True
            End If
        End If

        If TileState(3, 3) = 0 Then
            If TileState(2, 3) <> 0 Or TileState(1, 3) <> 0 Or TileState(0, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 3) = 0 Then
            If TileState(1, 3) <> 0 Or TileState(0, 3) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 3) = 0 Then
            If TileState(0, 3) <> 0 Then
                Return True
            End If
        End If


        If TileState(0, 3) = 0 Then
            If TileState(0, 2) <> 0 Or TileState(0, 1) <> 0 Or TileState(0, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(0, 2) = 0 Then
            If TileState(0, 1) <> 0 Or TileState(0, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(0, 1) = 0 Then
            If TileState(0, 0) <> 0 Then
                Return True
            End If
        End If

        If TileState(1, 3) = 0 Then
            If TileState(1, 2) <> 0 Or TileState(1, 1) <> 0 Or TileState(1, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 2) = 0 Then
            If TileState(1, 1) <> 0 Or TileState(1, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(1, 1) = 0 Then
            If TileState(1, 0) <> 0 Then
                Return True
            End If
        End If

        If TileState(2, 3) = 0 Then
            If TileState(2, 2) <> 0 Or TileState(2, 1) <> 0 Or TileState(2, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 2) = 0 Then
            If TileState(2, 1) <> 0 Or TileState(2, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(2, 1) = 0 Then
            If TileState(2, 0) <> 0 Then
                Return True
            End If
        End If

        If TileState(3, 3) = 0 Then
            If TileState(3, 2) <> 0 Or TileState(3, 1) <> 0 Or TileState(3, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(3, 2) = 0 Then
            If TileState(3, 1) <> 0 Or TileState(3, 0) <> 0 Then
                Return True
            End If
        End If
        If TileState(3, 1) = 0 Then
            If TileState(3, 0) <> 0 Then
                Return True
            End If
        End If



        If TileState(0, 0) = TileState(1, 0) And TileState(0, 0) <> 0 Then
            Return True
        End If
        If TileState(1, 0) = TileState(2, 0) And TileState(1, 0) <> 0 Then
            Return True
        End If
        If TileState(2, 0) = TileState(3, 0) And TileState(2, 0) <> 0 Then
            Return True
        End If

        If TileState(0, 1) = TileState(1, 1) And TileState(0, 1) <> 0 Then
            Return True
        End If
        If TileState(1, 1) = TileState(2, 1) And TileState(1, 1) <> 0 Then
            Return True
        End If
        If TileState(2, 1) = TileState(3, 1) And TileState(2, 1) <> 0 Then
            Return True
        End If

        If TileState(0, 2) = TileState(1, 2) And TileState(0, 2) <> 0 Then
            Return True
        End If
        If TileState(1, 2) = TileState(2, 2) And TileState(1, 2) <> 0 Then
            Return True
        End If
        If TileState(2, 2) = TileState(3, 2) And TileState(2, 2) <> 0 Then
            Return True
        End If

        If TileState(0, 3) = TileState(1, 3) And TileState(0, 3) <> 0 Then
            Return True
        End If
        If TileState(1, 3) = TileState(2, 3) And TileState(1, 3) <> 0 Then
            Return True
        End If
        If TileState(2, 3) = TileState(3, 3) And TileState(2, 3) <> 0 Then
            Return True
        End If


        If TileState(3, 0) = TileState(2, 0) And TileState(3, 0) <> 0 Then
            Return True
        End If
        If TileState(2, 0) = TileState(1, 0) And TileState(2, 0) <> 0 Then
            Return True
        End If
        If TileState(1, 0) = TileState(0, 0) And TileState(1, 0) <> 0 Then
            Return True
        End If

        If TileState(3, 1) = TileState(2, 1) And TileState(3, 1) <> 0 Then
            Return True
        End If
        If TileState(2, 1) = TileState(1, 1) And TileState(2, 1) <> 0 Then
            Return True
        End If
        If TileState(1, 1) = TileState(0, 1) And TileState(1, 1) <> 0 Then
            Return True
        End If

        If TileState(3, 2) = TileState(2, 2) And TileState(3, 2) <> 0 Then
            Return True
        End If
        If TileState(2, 2) = TileState(1, 2) And TileState(2, 2) <> 0 Then
            Return True
        End If
        If TileState(1, 2) = TileState(0, 2) And TileState(1, 2) <> 0 Then
            Return True
        End If

        If TileState(3, 3) = TileState(2, 3) And TileState(3, 3) <> 0 Then
            Return True
        End If
        If TileState(2, 3) = TileState(1, 3) And TileState(2, 3) <> 0 Then
            Return True
        End If
        If TileState(1, 3) = TileState(0, 3) And TileState(1, 3) <> 0 Then
            Return True
        End If


        If TileState(0, 0) = TileState(0, 1) And TileState(0, 0) <> 0 Then
            Return True
        End If
        If TileState(0, 1) = TileState(0, 2) And TileState(0, 1) <> 0 Then
            Return True
        End If
        If TileState(0, 2) = TileState(0, 3) And TileState(0, 3) <> 0 Then
            Return True
        End If

        If TileState(1, 0) = TileState(1, 1) And TileState(1, 0) <> 0 Then
            Return True
        End If
        If TileState(1, 1) = TileState(1, 2) And TileState(1, 1) <> 0 Then
            Return True
        End If
        If TileState(1, 2) = TileState(1, 3) And TileState(1, 3) <> 0 Then
            Return True
        End If

        If TileState(2, 0) = TileState(2, 1) And TileState(2, 0) <> 0 Then
            Return True
        End If
        If TileState(2, 1) = TileState(2, 2) And TileState(2, 1) <> 0 Then
            Return True
        End If
        If TileState(2, 2) = TileState(2, 3) And TileState(2, 3) <> 0 Then
            Return True
        End If

        If TileState(3, 0) = TileState(3, 1) And TileState(3, 0) <> 0 Then
            Return True
        End If
        If TileState(3, 1) = TileState(3, 2) And TileState(3, 1) <> 0 Then
            Return True
        End If
        If TileState(3, 2) = TileState(3, 3) And TileState(3, 3) <> 0 Then
            Return True
        End If


        If TileState(0, 3) = TileState(0, 2) And TileState(0, 3) <> 0 Then
            Return True
        End If
        If TileState(0, 2) = TileState(0, 1) And TileState(0, 2) <> 0 Then
            Return True
        End If
        If TileState(0, 1) = TileState(0, 0) And TileState(0, 1) <> 0 Then
            Return True
        End If

        If TileState(1, 3) = TileState(1, 2) And TileState(1, 3) <> 0 Then
            Return True
        End If
        If TileState(1, 2) = TileState(1, 1) And TileState(1, 2) <> 0 Then
            Return True
        End If
        If TileState(1, 1) = TileState(1, 0) And TileState(1, 1) <> 0 Then
            Return True
        End If

        If TileState(2, 3) = TileState(2, 2) And TileState(2, 3) <> 0 Then
            Return True
        End If
        If TileState(2, 2) = TileState(2, 1) And TileState(2, 2) <> 0 Then
            Return True
        End If
        If TileState(2, 1) = TileState(2, 0) And TileState(2, 1) <> 0 Then
            Return True
        End If

        If TileState(3, 3) = TileState(3, 2) And TileState(3, 3) <> 0 Then
            Return True
        End If
        If TileState(3, 2) = TileState(3, 1) And TileState(3, 2) <> 0 Then
            Return True
        End If
        If TileState(3, 1) = TileState(3, 0) And TileState(3, 1) <> 0 Then
            Return True
        End If

        Return False
    End Function

    Private Sub scoreAdd(ByVal addition)
        score = score + addition
        ScoreText.Text = score
        If score > best Then
            best = score
            BestText.Text = best
            bestChanged = True
        End If
    End Sub

    Private Function gameWon()
        If TileState(0, 0) = 2048 Then
            Return True
        End If

        If TileState(0, 1) = 2048 Then
            Return True
        End If

        If TileState(0, 2) = 2048 Then
            Return True
        End If

        If TileState(0, 3) = 2048 Then
            Return True
        End If


        If TileState(1, 0) = 2048 Then
            Return True
        End If

        If TileState(1, 1) = 2048 Then
            Return True
        End If

        If TileState(1, 2) = 2048 Then
            Return True
        End If

        If TileState(1, 3) = 2048 Then
            Return True
        End If


        If TileState(2, 0) = 2048 Then
            Return True
        End If

        If TileState(2, 1) = 2048 Then
            Return True
        End If

        If TileState(2, 2) = 2048 Then
            Return True
        End If

        If TileState(2, 3) = 2048 Then
            Return True
        End If


        If TileState(3, 0) = 2048 Then
            Return True
        End If

        If TileState(3, 1) = 2048 Then
            Return True
        End If

        If TileState(3, 2) = 2048 Then
            Return True
        End If

        If TileState(3, 3) = 2048 Then
            Return True
        End If



        Return False
    End Function
End Class