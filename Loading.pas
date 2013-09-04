procedure TfrmMain.Load(strFileName : string);
var
   f,g,h,i : integer;
   pLayer : TLayerObjPtr;
   pFrameSet : TSingleFramePtr;
   pFrame : TIFramePtr;
   nFrameSetCount, nFramesCount : integer;
   x, y : integer;
   nWide, nHigh : integer;
   nType : integer;
   strInfo, strLayerName : string[255];
   fs : TFileStream;
   bRead : boolean;
   nActionCount : integer;
   pAction : TActionObjPtr;
   misc : integer;
   bLoadNew : boolean;     //used for later version where can load many BMPs into 1 layer
   bTrans : boolean;
   bFirstLayer : BOOLEAN;
   bMore : boolean;
   nSkip : integer;
   bNewFormat : boolean;

   bitty : TBitmap;
   ms : TMemoryStream;
begin
   m_bChanged := FALSE;
   m_bOld := FALSE;

   fs := TFileStream.Create(strFileName, fmOpenRead);

   fs.Read(f, sizeof(integer));
   fs.Read(g, sizeof(integer));
   fs.Read(h, sizeof(integer));
   //
   fs.Read(i, sizeof(integer));
   //
   bFirstLayer := TRUE;
   bNewFormat := FALSE;
   if (f <> ord('I')) or (g <> ord('H')) or (h <> ord('8')) then
   begin
      m_strMovieFileName := '';
      m_bSaved := FALSE;
      fs.Free;
      MessageBox(Application.Handle, pChar(strFileName + ' does not appear to be a valid TISFAT file.'), 'Tis an error', MB_OK or MB_ICONERROR);
      exit;
   end;
      if (i = ord('U')) then
      begin
         bFirstLayer := FALSE;
      end else
      if (i = ord('I')) then
      begin
         bFirstLayer := TRUE;
      end else
      if (i = ord('V')) then
      begin
         bFirstLayer := TRUE;
         bNewFormat := true;
      end else
      begin
         m_strMovieFileName := '';
         m_bSaved := FALSE;
         fs.Free;
         MessageBox(Application.Handle, pChar(strFileName + ' does not appear to be a valid TISFAT file.'), 'Tis an error', MB_OK or MB_ICONERROR);
         exit;
      end;
   fs.Read(nWide, sizeof(integer));
   fs.Read(nHigh, sizeof(integer));
   NewMovie(nWide, nHigh);
   //
   fs.Read(m_nFPS, sizeof(integer));
   frmToolBar.m_strFPS.Text := itoa(m_nFPS);
   //
   fs.Read(m_bgColor, sizeof(m_bgColor));
   while fs.Position < fs.Size do
   begin
      if bFirstLayer then
      begin
         pLayer := m_olLayers.Items[0];
      end else
      begin
         grdFrames.RowCount := grdFrames.RowCount+1;
         New(pLayer);
         m_olLayers.Add(pLayer);
      end;
      strInfo := '';

      fs.Read(i, sizeof(i));
      fs.Read(strLayerName, i);

      fs.Read(nType, sizeof(integer));
      if (nType = O_POLY) then
      begin
         nType := O_NOTHING;
      end;
      if (nType = O_T2STICK) then
      begin
         strInfo := '';
      end;
      fs.Read(nFrameSetCount, sizeof(integer));
      if not bFirstLayer then
      begin
         pLayer^ := TLayerObj.Create(nType, strInfo);
      end;
      pLayer^.m_strName := strLayerName;
      bRead := fALSE;

      bFirstLayer := FALSE;
      fs.Read(nActionCount, sizeof(nActionCount));
      for g := 0 to nActionCount-1 do
      begin
         new(pAction);
         pAction^ := TActionObj.Create();
         fs.Read(pAction^.m_nType, sizeof(pAction^.m_nType));
         fs.Read(pAction^.m_nFrameNo, sizeof(pAction^.m_nFrameNo));
         case pAction^.m_nType of
            A_JUMPTO,A_SHAKE: begin
                  fs.Read(pAction^.m_nParams[1], sizeof(pAction^.m_nParams[1]));
                  fs.Read(pAction^.m_nParams[2], sizeof(pAction^.m_nParams[2]));
                  fs.Read(pAction^.m_nParams[3], sizeof(pAction^.m_nParams[3]));
               end;
            A_LOADNEW: begin
                  fs.Read(misc, sizeof(misc));
                  fs.Read(pAction^.m_strParam, misc);
               end;
            A_OLD : fs.Read(pAction^.m_nParams[1], sizeof(pAction^.m_nParams[1]));
         end;
         pLayer^.m_olActions.Add(pAction);
      end;

      for f := 0 to nFrameSetCount-1 do
      begin
         New(pFrameSet);
         pLayer^.m_olFrames.Add(pFrameSet);
         pFrameSet^ := TSingleFrame.Create();
         pFrameSet^.m_Type := 6;
         fs.Read(nFramesCount, sizeof(integer));
         for g := 0 to nFramesCount-1 do
         begin
            New(pFrame);
            pFrameSet^.m_Frames.Add(pFrame);
            pFrame^ := TIFrame.Create();
            pFrame^.m_nType := nType;
            if (nType = O_NOTHING) then pFrame^.m_nType := O_POLY;
            fs.Read(pFrame^.m_nOnion, sizeof(pFrame^.m_nOnion));
            fs.Read(pFrame^.m_FrameNo, sizeof(pFrame^.m_FrameNo));
            if (pLayer^.m_nType = O_EDITVIDEO) then
            begin
               New(TEditVideoObjPtr(pFrame^.m_pObject));
               TEditVideoObjPtr(pFrame^.m_pObject)^ := TEditVideoObj.Create(frmCanvas);
               with TEditVideoObjPtr(pFrame^.m_pObject)^ do
               begin
                  fs.Read(i, sizeof(i));
                  fs.Read(TEditVideoObjPtr(pFrame^.m_pObject)^.m_strFileName, i);
                  for i := 1 to 4 do
                  begin
                     fs.Read(x, sizeof(x));
                     fs.Read(y, sizeof(y));
                     Pnt(i)^.Left := x;
                     Pnt(i)^.Top := y;
                  end;
                  //
               end;
            end;
            if (pLayer^.m_nType = O_RECTANGLE) then
            begin
               New(TSquareObjPtr(pFrame^.m_pObject));
               TSquareObjPtr(pFrame^.m_pObject)^ := TSquareObj.Create(frmCanvas);
               with TSquareObjPtr(pFrame^.m_pObject)^ do
               begin
                  for i := 1 to 4 do
                  begin
                     fs.Read(x, sizeof(x));
                     fs.Read(y, sizeof(y));
                     Pnt(i)^.Left := x;
                     Pnt(i)^.Top := y;
                  end;
                  fs.Read(m_nLineWidth, sizeof(m_nLineWidth));
                  fs.Read(m_InColour, sizeof(m_InColour));
                  fs.Read(m_OutColour, sizeof(m_OutColour));
                  //
                  fs.Read(m_styleInner, sizeof(m_styleInner));
                  fs.Read(m_styleOuter, sizeof(m_styleOuter));
                  //
                  if (bNewFormat) then
                  begin
                     fs.Read(m_angle, sizeof(m_angle));
                     fs.Read(m_alpha, sizeof(m_alpha));
                     fs.Read(m_aliased, sizeof(m_aliased));
                  end;
               end;
            end;
            if (pLayer^.m_nType = O_LINE) then
            begin
               New(TLineObjPtr(pFrame^.m_pObject));
               TLineObjPtr(pFrame^.m_pObject)^ := TLineObj.Create(frmCanvas);
               with TLineObjPtr(pFrame^.m_pObject)^ do
               begin
                  for i := 1 to 2 do
                  begin
                     fs.Read(x, sizeof(x));
                     fs.Read(y, sizeof(y));
                     Pnt(i)^.Left := x;
                     Pnt(i)^.Top := y;
                  end;
                  fs.Read(m_nLineWidth, sizeof(m_nLineWidth));
                  fs.Read(m_Colour, sizeof(m_Colour));
                  if (bNewFormat) then
                  begin
                     fs.Read(m_angle, sizeof(m_angle));
                     fs.Read(m_alpha, sizeof(m_alpha));
                     fs.Read(m_aliased, sizeof(m_aliased));
                  end;
               end;
            end;
            if (pLayer^.m_nType = O_EXPLODE) then
            begin
               New(TExplodeObjPtr(pFrame^.m_pObject));
               TExplodeObjPtr(pFrame^.m_pObject)^ := TExplodeObj.Create(frmCanvas, g = 0);
               with TExplodeObjPtr(pFrame^.m_pObject)^ do
               begin
                  for i := 1 to 2 do
                  begin
                     fs.Read(x, sizeof(x));
                     fs.Read(y, sizeof(y));
                     Pnt(i)^.Left := x;
                     Pnt(i)^.Top := y;
                  end;
                  fs.Read(m_nMidX, sizeof(m_nMidX));
                  fs.Read(m_nMidY, sizeof(m_nMidY));
                  if (g = 0) then
                  begin
                     InitParts;
                  end;
               end;
            end;
            if (pLayer^.m_nType = O_BITMAP) then
            begin
               fs.Read(bLoadNew, sizeof(bLoadNew));   //unused for now
               if not bRead then
               begin
                  if (bLoadNew) then
                     LoadBitmap(TBitmanPtr(pLayer^.m_pTempObject)^.Imarge, fs, TBitmanPtr(pLayer^.m_pTempObject)^.ms)
                  else
                     LoadBitmapOld(TBitmanPtr(pLayer^.m_pTempObject)^.Imarge, fs);
                  //
                  fs.Read(bTrans, sizeof(bTrans));
                  //
                  bRead := TRUE;
               end;
               New(TBitmanPtr(pFrame^.m_pObject));
               TBitmanPtr(pFrame^.m_pObject)^ := TBitman.Create(frmCanvas, '', TBitManPtr(pLayer^.m_pTempObject)^.Imarge.GetWidth, TBitManPtr(pLayer^.m_pTempObject)^.Imarge.GetHeight);
               with TBitmanPtr(pFrame^.m_pObject)^ do
               begin
                  for i := 1 to 4 do
                  begin
                     fs.Read(x, sizeof(x));
                     fs.Read(y, sizeof(y));
                     TBitmanPtr(pFrame^.m_pObject)^.Pnt(i)^.Left := x;
                     TBitmanPtr(pFrame^.m_pObject)^.Pnt(i)^.Top := y;
                  end;
                  if (bNewFormat) then
                  begin
                     fs.Read(m_angle, sizeof(m_angle));
                     fs.Read(m_alpha, sizeof(m_alpha));
                     fs.Read(m_aliased, sizeof(m_aliased));
                  end;
               end;
            end;

            if (pLayer^.m_nType = O_STICKMANBMP) then
            begin
               New(TStickManBMPPtr(pFrame^.m_pObject));
               TStickManBMPPtr(pFrame^.m_pObject)^ := TStickManBMP.Create(frmCanvas, 0,0,0,0,0,0,0,0,0);
               if not bRead then
               begin
                  LoadBitmap(TStickManBMPPtr(pLayer^.m_pTempObject)^.m_FaceClosed, fs, TStickManBMPPtr(pLayer^.m_pTempObject)^.ms);
                  LoadBitmap(TStickManBMPPtr(pLayer^.m_pTempObject)^.m_FaceOpen, fs, TStickManBMPPtr(pLayer^.m_pTempObject)^.ms);
                  bRead := TRUE;
               end;
               with TStickManBMPPtr(pFrame^.m_pObject)^ do
               begin
                  fs.Read(m_nHeadDiam, sizeof(m_nHeadDiam));
                  for i := 1 to 10 do
                  begin
                     fs.Read(x, sizeof(x));
                     fs.Read(y, sizeof(y));
                     Pnt(i)^.Left := x;
                     Pnt(i)^.Top := y;
                  end;
                  for i := 1 to 10 do
                  begin
                     fs.Read(Wid[i], sizeof(Wid[i]));
                  end;
                  for i := 1 to 9 do
                  begin
                     fs.Read(Lng[i], sizeof(Lng[i]));
                  end;
                  fs.Read(m_OutColour, sizeof(m_OutColour));
                  fs.Read(m_bMouthOpen, sizeof(m_bMouthOpen));
                  fs.Read(m_bFlipped, sizeof(m_bFlipped));
                  if (bNewFormat) then
                  begin
                     fs.Read(m_angle, sizeof(m_angle));
                     fs.Read(m_alpha, sizeof(m_alpha));
                     fs.Read(m_aliased, sizeof(m_aliased));
                  end;
               end;
            end;

            if (pLayer^.m_nType = O_TEXT) then
            begin
               New(TTextObjPtr(pFrame^.m_pObject));
               TTextObjPtr(pFrame^.m_pObject)^ := TTextObj.Create(frmCanvas);
               with TTextObjPtr(pFrame^.m_pObject)^ do
               begin
                  for i := 1 to 4 do
                  begin
                     fs.Read(x, sizeof(x));
                     fs.Read(y, sizeof(y));
                     Pnt(i)^.Left := x;
                     Pnt(i)^.Top := y;
                  end;
                  fs.Read(m_InColour, sizeof(m_InColour));
                  fs.Read(m_OutColour, sizeof(m_OutColour));
                  fs.Read(m_styleOuter, sizeof(m_styleOuter));
                  fs.Read(m_FontStyle, sizeof(m_FontStyle));
                  fs.Read(i, sizeof(i));
                  fs.Read(m_strFontName, i);
                  fs.Read(i, sizeof(i));
                  fs.Read(m_strCaption, i);
                  if (bNewFormat) then
                  begin
                     fs.Read(m_angle, sizeof(m_angle));
                     fs.Read(m_alpha, sizeof(m_alpha));
                     fs.Read(m_aliased, sizeof(m_aliased));
                  end;
               end;
            end;
            if (pLayer^.m_nType = O_OVAL) then
            begin
               New(TOvalObjPtr(pFrame^.m_pObject));
               TOvalObjPtr(pFrame^.m_pObject)^ := TOvalObj.Create(frmCanvas);
               with TOvalObjPtr(pFrame^.m_pObject)^ do
               begin
                  for i := 1 to 4 do
                  begin
                     fs.Read(x, sizeof(x));
                     fs.Read(y, sizeof(y));
                     Pnt(i)^.Left := x;
                     Pnt(i)^.Top := y;
                  end;
                  fs.Read(m_nLineWidth, sizeof(m_nLineWidth));
                  fs.Read(m_InColour, sizeof(m_InColour));
                  fs.Read(m_OutColour, sizeof(m_OutColour));
                  //
                  fs.Read(m_styleInner, sizeof(m_styleInner));
                  fs.Read(m_styleOuter, sizeof(m_styleOuter));
                  //
                  if (bNewFormat) then
                  begin
                     fs.Read(m_angle, sizeof(m_angle));
                     fs.Read(m_alpha, sizeof(m_alpha));
                     fs.Read(m_aliased, sizeof(m_aliased));
                  end;
               end;
            end;
            if (pLayer^.m_nType = O_STICKMAN) then
            begin
               New(TStickManPtr(pFrame^.m_pObject));
               TStickManPtr(pFrame^.m_pObject)^ := TStickMan.Create(frmCanvas, 0,0,0,0,0,0,0,0,0);
               with TStickManPtr(pFrame^.m_pObject)^ do
               begin
                  fs.Read(m_nHeadDiam, sizeof(m_nHeadDiam));
                  for i := 1 to 10 do
                  begin
                     fs.Read(x, sizeof(x));
                     fs.Read(y, sizeof(y));
                     Pnt(i)^.Left := x;
                     Pnt(i)^.Top := y;
                  end;
                  for i := 1 to 10 do
                  begin
                     fs.Read(Wid[i], sizeof(Wid[i]));
                  end;
                  for i := 1 to 9 do
                  begin
                     fs.Read(Lng[i], sizeof(Lng[i]));
                  end;
                  fs.Read(m_InColour, sizeof(m_InColour));
                  fs.Read(m_OutColour, sizeof(m_OutColour));
                  if (bNewFormat) then
                  begin
                     fs.Read(m_angle, sizeof(m_angle));
                     fs.Read(m_alpha, sizeof(m_alpha));
                     fs.Read(m_aliased, sizeof(m_aliased));
                  end;
               end;
            end;
            if (pLayer^.m_nType = O_T2STICK) then
            begin
               New(TLimbListPtr(pFrame^.m_pObject));
               TLimbListPtr(pFrame^.m_pObject)^ := TLimbList.Create();
               if (not bRead) then
               begin
                  TLimbListPtr(pLAyer^.m_pTempObject)^.Read(fs);
                  TLimbListPtr(pFrame^.m_pObject)^.CopyFrom(TLimbListPtr(pLayer^.m_pTempObject)^);
                  bRead := TRUE;
               end else
               begin
                  TLimbListPtr(pFrame^.m_pObject)^.Read(fs);
               end;
            end;
            if (pLayer^.m_nType = O_SPECIALSTICK) then
            begin
               New(TSpecialStickManPtr(pFrame^.m_pObject));
               TSpecialStickManPtr(pFrame^.m_pObject)^ := TSpecialStickMan.Create(frmCanvas, 0,0,0,0,0,0,0,0,0);
               with TSpecialStickManPtr(pFrame^.m_pObject)^ do
               begin
                  fs.REad(m_nDrawStyle, sizeof(m_nDrawStyle));
                           TSpecialStickManPtr(pLayer^.m_pObject)^.m_nDrawStyle := m_nDrawStyle;
                           TSpecialStickManPtr(pLayer^.m_pTempObject)^.m_nDrawStyle := m_nDrawStyle;
                  fs.REad(m_nLineWidth, sizeof(m_nLineWidth));
                  fs.Read(m_nHeadDiam, sizeof(m_nHeadDiam));
                  fs.Read(m_styleInner, sizeof(m_styleInner));
                  fs.Read(m_styleOuter, sizeof(m_styleOuter));
                  for i := 1 to 14 do
                  begin
                     fs.Read(x, sizeof(x));
                     fs.Read(y, sizeof(y));
                     Pnt(i)^.Left := x;
                     Pnt(i)^.Top := y;
                  end;
                  for i := 1 to 14 do
                  begin
                     fs.Read(Wid[i], sizeof(Wid[i]));
                  end;
                  for i := 1 to 13 do
                  begin
                     fs.Read(Lng[i], sizeof(Lng[i]));
                  end;
                  fs.Read(m_InColour, sizeof(m_InColour));
                  fs.Read(m_OutColour, sizeof(m_OutColour));
                  //
                  fs.Read(bMore, sizeof(bMore));
                  if (bMore) then
                  begin
                     fs.REad(nSkip, sizeof(nSkip));
                     fs.Seek(nSkip, soFromCurrent)
                  end;
                  //
                  if (bNewFormat) then
                  begin
                     fs.Read(m_angle, sizeof(m_angle));
                     fs.Read(m_alpha, sizeof(m_alpha));
                     fs.Read(m_aliased, sizeof(m_aliased));
                  end;
               end;
            end;
            if (pLayer^.m_nType = O_NOTHING) or (pLayer^.m_nType = O_POLY) then        ///POLY HACK!
            begin
               pLayer^.m_nType := O_POLY;
               New(TPolyObjPtr(pFrame^.m_pObject));
               fs.Read(i, sizeof(i));
               TPolyObjPtr(pFrame^.m_pObject)^ := TPolyObj.Create(frmCanvas, i);
               if (pLayer^.m_pObject = nil) then
               begin
                  new(TPolyObjPtr(pLayer^.m_pObject));
                  new(TPolyObjPtr(pLayer^.m_pTempObject));
                  TPolyObjPtr(pLayer^.m_pObject)^ := TPolyObj.Create(frmCanvas, i);
                  TPolyObjPtr(pLayer^.m_pTempObject)^ := TPolyObj.Create(frmCanvas, i);
               end;
               with TPolyObjPtr(pFrame^.m_pObject)^ do
               begin
                  for h := 0 to i-1 do
                  begin
                     fs.Read(x, sizeof(TLabel2Ptr(PntList.Items[h])^.Left));
                     fs.Read(y, sizeof(TLabel2Ptr(PntList.Items[h])^.Top));
                     TLabel2Ptr(PntList.Items[h])^.Left := x;
                     TLabel2Ptr(PntList.Items[h])^.Top := y;
                  end;
                  fs.Read(m_nLineWidth, sizeof(m_nLineWidth));
                  fs.Read(m_InColour, sizeof(m_InColour));
                  fs.Read(m_OutColour, sizeof(m_OutColour));
                  //
                  fs.Read(m_styleInner, sizeof(m_styleInner));
                  fs.Read(m_styleOuter, sizeof(m_styleOuter));
                  //
                  if (bNewFormat) then
                  begin
                     fs.Read(m_angle, sizeof(m_angle));
                     fs.Read(m_alpha, sizeof(m_alpha));
                     fs.Read(m_aliased, sizeof(m_aliased));
                  end;
                end;
            end;
         end;
      end;
   end;
   fs.Free;
   Render(1, TRUE);
end;