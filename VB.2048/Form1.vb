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
    Dim GameMode As String = "Classic"
    Dim winEnvironment As Boolean = False

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
        MenuButton.Image = New Bitmap("data/gui/menu.png")
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
        createScore(scorereader)
    End Sub

    Private Sub createScore(ByVal reader)
        Try
            If ErrorToString.Contains("Could not find a part of the path") Then System.IO.Directory.CreateDirectory("data/score/") Else reader.Close()
        Catch : End Try
        createScore()
    End Sub

    Private Sub createScore()
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
        For x = 0 To 3
            For y = 0 To 3
                Dim TileToSet As Object = generateTileObject(x, y)
                TileToSet.Location = GridPos(x, y)
            Next y
        Next x
    End Sub

    Private Sub clearAllTiles()
        For x = 0 To 3
            For y = 0 To 3
                Dim TileToClear As Object = generateTileObject(x, y)
                TileToClear.Image = New Bitmap("data/tiles/tile_0.png")
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
        If playing = True And sender IsNot Nothing Then
            If MsgBox("Are you sure you want to start a new game? You will lose the current game.", MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.Yes Then
                playing = False
            Else
                Exit Sub
            End If
        End If
        If GameMode = "Classic" Then
            Banner.Image = New Bitmap("data/gui/banner.png")
            Me.BackColor = BGColor
            Grid.BackColor = BGColor
            Banner.BackColor = BGColor
            ScoreBox.BackColor = BGColor
            BestBox.BackColor = BGColor
            NewGameButton.BackgroundImage = Nothing
            NewGameButton.Image = New Bitmap("data/gui/newgame.png")
            playing = False
            KeepGoingButton.Visible = False
            winEnvironment = False
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
        End If
        If GameMode = "X-Tile" Then
            Banner.Image = New Bitmap("data/gui/banner.png")
            Me.BackColor = BGColor : Banner.BackColor = BGColor
            NewGameButton.BackgroundImage = Nothing
            NewGameButton.Image = New Bitmap("data/gui/newgame.png")
            playing = False
            KeepGoingButton.Visible = False
            bestChanged = False
            score = 0
            ScoreText.Text = score
            Call clearAllTiles()
firsttilesgenx:
            Dim Tile1 As System.Object = generateTileObject(CInt(Rnd() * 3), CInt(Rnd() * 3))
            Dim Tile2 As System.Object = generateTileObject(CInt(Rnd() * 3), CInt(Rnd() * 3))
            If Tile1 Is Tile2 Then GoTo firsttilesgenx
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
            Dim XTile As Object = generateTileObject(CInt(Rnd() * 3), CInt(Rnd() * 3))
            XTile.Image = New Bitmap("data/tiles/tile_1.png")
            TileX = Microsoft.VisualBasic.Mid(XTile.Name.ToString(), 5, 1)
            TileY = Microsoft.VisualBasic.Mid(XTile.Name.ToString(), 6, 1)
            TileState(TileX, TileY) = 1
            playing = True
        End If
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
                        For y = 0 To 3
                            For x = 0 To 2
                                If TileState(x, y) = 0 Then
                                    Dim tempbool As Boolean = False
                                    For k = x + 1 To 3
                                        If TileState(k, y) <> 0 Then
                                            tempbool = True
                                        End If
                                    Next k
                                    If tempbool Then
                                        For k = x To 3
                                            Dim Tile As Object = generateTileObject(k, y)
                                            If k < 3 Then
                                                Tile.Image = New Bitmap("data/tiles/tile_" & TileState(k + 1, y) & ".png")
                                                TileState(k, y) = TileState(k + 1, y)
                                            Else
                                                Tile.Image = New Bitmap("data/tiles/tile_0.png")
                                                TileState(k, y) = 0
                                            End If
                                            MoveMade = True
                                        Next k
                                        If MoveMade Then GoTo left
                                    End If
                                End If
                            Next x
                        Next y

                        If MoveMade = True Or mergeTilesLeft() = True Then
                            Call generateNewTile()
                        End If

                    Case Keys.Up
                        Dim MoveMade As Boolean = False
up:
                        For x = 0 To 3
                            For y = 0 To 2
                                If TileState(x, y) = 0 Then
                                    Dim tempbool As Boolean = False
                                    For k = y + 1 To 3
                                        If TileState(x, k) <> 0 Then
                                            tempbool = True
                                        End If
                                    Next k
                                    If tempbool Then
                                        For k = y To 3
                                            Dim Tile As Object = generateTileObject(x, k)
                                            If k < 3 Then
                                                Tile.Image = New Bitmap("data/tiles/tile_" & TileState(x, k + 1) & ".png")
                                                TileState(x, k) = TileState(x, k + 1)
                                            Else
                                                Tile.Image = New Bitmap("data/tiles/tile_0.png")
                                                TileState(x, k) = 0
                                            End If
                                            MoveMade = True
                                        Next k
                                        If MoveMade Then GoTo up
                                    End If
                                End If
                            Next y
                        Next x

                        If MoveMade = True Or mergeTilesUp() = True Then
                            Call generateNewTile()
                        End If

                    Case Keys.Right
                        Dim MoveMade As Boolean = False
right:
                        For y = 0 To 3
                            For x = 3 To 1 Step -1
                                If TileState(x, y) = 0 Then
                                    Dim tempbool As Boolean = False
                                    For k = x - 1 To 0 Step -1
                                        If TileState(k, y) <> 0 Then
                                            tempbool = True
                                        End If
                                    Next k
                                    If tempbool Then
                                        For k = x To 0 Step -1
                                            Dim Tile As Object = generateTileObject(k, y)
                                            If k > 0 Then
                                                Tile.Image = New Bitmap("data/tiles/tile_" & TileState(k - 1, y) & ".png")
                                                TileState(k, y) = TileState(k - 1, y)
                                            Else
                                                Tile.Image = New Bitmap("data/tiles/tile_0.png")
                                                TileState(k, y) = 0
                                            End If
                                            MoveMade = True
                                        Next k
                                        If MoveMade Then GoTo right
                                    End If
                                End If
                            Next x
                        Next y

                        If MoveMade = True Or mergeTilesRight() = True Then
                            Call generateNewTile()
                        End If

                    Case Keys.Down
                        Dim MoveMade As Boolean = False
down:
                        For x = 0 To 3
                            For y = 3 To 1 Step -1
                                If TileState(x, y) = 0 Then
                                    Dim tempbool As Boolean = False
                                    For k = y - 1 To 0 Step -1
                                        If TileState(x, k) <> 0 Then
                                            tempbool = True
                                        End If
                                    Next k
                                    If tempbool Then
                                        For k = y To 0 Step -1
                                            Dim Tile As Object = generateTileObject(x, k)
                                            If k > 0 Then
                                                Tile.Image = New Bitmap("data/tiles/tile_" & TileState(x, k - 1) & ".png")
                                                TileState(x, k) = TileState(x, k - 1)
                                            Else
                                                Tile.Image = New Bitmap("data/tiles/tile_0.png")
                                                TileState(x, k) = 0
                                            End If
                                            MoveMade = True
                                        Next k
                                        If MoveMade Then GoTo down
                                    End If
                                End If
                            Next y
                        Next x

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
                    KeepGoingButton.Visible = True
                    winEnvironment = True
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

        If merged Then
check:
            For y = 0 To 3
                For x = 0 To 2
                    If TileState(x, y) = 0 Then
                        Dim tempbool As Boolean = False
                        For k = x + 1 To 3
                            If TileState(k, y) <> 0 Then
                                tempbool = True
                            End If
                        Next k
                        If tempbool Then
                            For k = x To 3
                                Dim Tile As Object = generateTileObject(k, y)
                                If k < 3 Then
                                    Tile.Image = New Bitmap("data/tiles/tile_" & TileState(k + 1, y) & ".png")
                                    TileState(k, y) = TileState(k + 1, y)
                                Else
                                    Tile.Image = New Bitmap("data/tiles/tile_0.png")
                                    TileState(k, y) = 0
                                End If
                            Next k
                            GoTo check
                        End If
                    End If
                Next x
            Next y
        End If

        Call scoreAdd(scoreAddition)
        Return merged
    End Function

    Private Function mergeTilesRight()
        Dim merged As Boolean = False
        Dim scoreAddition As Integer

        For y = 0 To 3
            For x = 3 To 1 Step -1
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
            Next x
        Next y

        If merged Then
check:
            For y = 0 To 3
                For x = 3 To 1 Step -1
                    If TileState(x, y) = 0 Then
                        Dim tempbool As Boolean = False
                        For k = x - 1 To 0 Step -1
                            If TileState(k, y) <> 0 Then
                                tempbool = True
                            End If
                        Next k
                        If tempbool Then
                            For k = x To 0 Step -1
                                Dim Tile As Object = generateTileObject(k, y)
                                If k > 0 Then
                                    Tile.Image = New Bitmap("data/tiles/tile_" & TileState(k - 1, y) & ".png")
                                    TileState(k, y) = TileState(k - 1, y)
                                Else
                                    Tile.Image = New Bitmap("data/tiles/tile_0.png")
                                    TileState(k, y) = 0
                                End If
                            Next k
                            GoTo check
                        End If
                    End If
                Next x
            Next y
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
            For x = 0 To 3
                For y = 0 To 2
                    If TileState(x, y) = 0 Then
                        Dim tempbool As Boolean = False
                        For k = y + 1 To 3
                            If TileState(x, k) <> 0 Then
                                tempbool = True
                            End If
                        Next k
                        If tempbool Then
                            For k = y To 3
                                Dim Tile As Object = generateTileObject(x, k)
                                If k < 3 Then
                                    Tile.Image = New Bitmap("data/tiles/tile_" & TileState(x, k + 1) & ".png")
                                    TileState(x, k) = TileState(x, k + 1)
                                Else
                                    Tile.Image = New Bitmap("data/tiles/tile_0.png")
                                    TileState(x, k) = 0
                                End If
                            Next k
                            GoTo check
                        End If
                    End If
                Next y
            Next x
        End If

        Call scoreAdd(scoreAddition)
        Return merged
    End Function

    Private Function mergeTilesDown()
        Dim merged As Boolean = False
        Dim scoreAddition As Integer

        For x = 0 To 3
            For y = 3 To 1 Step -1
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
            Next y
        Next x

        If merged = True Then
check:
            For x = 0 To 3
                For y = 3 To 1 Step -1
                    If TileState(x, y) = 0 Then
                        Dim tempbool As Boolean = False
                        For k = y - 1 To 0 Step -1
                            If TileState(x, k) <> 0 Then
                                tempbool = True
                            End If
                        Next k
                        If tempbool Then
                            For k = y To 0 Step -1
                                Dim Tile As Object = generateTileObject(x, k)
                                If k > 0 Then
                                    Tile.Image = New Bitmap("data/tiles/tile_" & TileState(x, k - 1) & ".png")
                                    TileState(x, k) = TileState(x, k - 1)
                                Else
                                    Tile.Image = New Bitmap("data/tiles/tile_0.png")
                                    TileState(x, k) = 0
                                End If
                            Next k
                            GoTo check
                        End If
                    End If
                Next y
            Next x
        End If

        Call scoreAdd(scoreAddition)
        Return merged
    End Function

    Private Function movePossible()
        For y = 0 To 3
            For x = 0 To 2
                If TileState(x, y) = 0 Then
                    For k = x + 1 To 3
                        If TileState(k, y) <> 0 Then
                            Return True
                        End If
                    Next k
                End If
            Next x
        Next y

        For x = 0 To 3
            For y = 0 To 2
                If TileState(x, y) = 0 Then
                    For k = y + 1 To 3
                        If TileState(x, k) <> 0 Then
                            Return True
                        End If
                    Next k
                End If
            Next y
        Next x

        For y = 0 To 3
            For x = 3 To 1 Step -1
                If TileState(x, y) = 0 Then
                    For k = x - 1 To 0 Step -1
                        If TileState(k, y) <> 0 Then
                            Return True
                        End If
                    Next k
                End If
            Next x
        Next y

        For x = 0 To 3
            For y = 3 To 1 Step -1
                If TileState(x, y) = 0 Then
                    For k = y - 1 To 0 Step -1
                        If TileState(x, k) <> 0 Then
                            Return True
                        End If
                    Next k
                End If
            Next y
        Next x

        For y = 0 To 3
            For x = 0 To 2
                If TileState(x, y) = TileState(x + 1, y) And TileState(x, y) <> 0 Then
                    Return True
                End If
            Next
        Next

        For y = 0 To 3
            For x = 3 To 1 Step -1
                If TileState(x, y) = TileState(x - 1, y) And TileState(x, y) <> 0 Then
                    Return True
                End If
            Next x
        Next y

        For x = 0 To 3
            For y = 0 To 2
                If TileState(x, y) = TileState(x, y + 1) And TileState(x, y) <> 0 Then
                    Return True
                End If
            Next y
        Next x

        For x = 0 To 3
            For y = 3 To 1 Step -1
                If TileState(x, y) = TileState(x, y - 1) And TileState(x, y) <> 0 Then
                    Return True
                End If
            Next y
        Next x

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

    Private Sub KeepGoingButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeepGoingButton.Click
        Banner.Image = New Bitmap("data/gui/banner.png")
        Me.BackColor = BGColor
        Grid.BackColor = BGColor
        Banner.BackColor = BGColor
        ScoreBox.BackColor = BGColor
        BestBox.BackColor = BGColor
        NewGameButton.BackgroundImage = Nothing
        NewGameButton.Image = New Bitmap("data/gui/newgame.png")
        KeepGoingButton.Visible = False
        winEnvironment = False
        playing = True
    End Sub

    Private Sub MenuButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuButton.Click
        ContextMenu.Show(Me, MenuButton.Location.X + 12, MenuButton.Location.Y + 9)
    End Sub

    Private Sub DayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DayToolStripMenuItem.Click
        BGColor = Color.FromArgb(250, 248, 239)
        Call fillGridPos()
        Call setTilesPos()
        If Not winEnvironment Then
            Me.BackColor = BGColor
            Grid.BackColor = BGColor
            Banner.BackColor = BGColor
            ScoreBox.BackColor = BGColor
            BestBox.BackColor = BGColor
            MenuButton.Image = New Bitmap("data/gui/menu.png")
        End If
    End Sub

    Private Sub NightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NightToolStripMenuItem.Click
        BGColor = Color.FromArgb(49, 36, 25)
        Call fillGridPos()
        Call setTilesPos()
        Me.BackColor = BGColor
        Grid.BackColor = BGColor
        Banner.BackColor = BGColor
        ScoreBox.BackColor = BGColor
        BestBox.BackColor = BGColor
        MenuButton.Image = New Bitmap("data/gui/menu_night.png")
    End Sub

    Private Sub XTileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XTileToolStripMenuItem.Click
        If playing Then
            If MsgBox("If you change mode, your current game will be discarded." & vbCrLf & "Are you sure you want to continue?", MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        GameMode = "X-Tile"
        NewGame_Click(Nothing, EventArgs.Empty)
    End Sub

    Private Sub ClassicToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClassicToolStripMenuItem.Click
        If playing Then
            If MsgBox("If you change mode, your current game will be discarded." & vbCrLf & "Are you sure you want to continue?", MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        GameMode = "Classic"
        NewGame_Click(Nothing, EventArgs.Empty)
    End Sub
End Class