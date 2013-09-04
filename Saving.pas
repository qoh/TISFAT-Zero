procedure TfrmMain.Save(strFileName : string; autosave : boolean);
var
   f,g,h,i : integer;
   pLayer : TLayerObjPtr;
   pFrameSet : TSingleFramePtr;
   pFrame : TIFramePtr;
   pAction : TActionObjPtr;
   fs : TFileStream;
   bWritten : BOOLEAN;
   misc : integer;
   nFPS : byte;
   //
   bTrans : boolean;
   bMore : boolean;
   //
   bitty : TBitmap;
   bittyHand : TGPGraphics;
   wide,high : integer;
   x,y : integer;
begin
   if (not autosave) and FileExists(strFileName) and (not m_bSaved) then
   begin
      if MEssageBox(Application.Handle, pChar(strFileName + ' already exists, overwrite?'), pChar('Overwrite?'), MB_YESNOCANCEL or MB_ICONQUESTION) <> IDYES then
      begin
         exit;
      end;
   end;
   DeleteFile(strFileName);
   fs := TFileStream.Create(strFileName, fmCreate);

   nFPS := atoi(frmToolBar.m_strFPS.Text);
   f := ord('I');
   g := ord('H');
   h := ord('8');
   //
   i := ord('V'); //U,I, V
   // no more FPS here
   fs.Write(f, sizeof(integer));
   fs.Write(g, sizeof(integer));
   fs.Write(h, sizeof(integer));
   fs.Write(i, sizeof(integer));

   f := m_nMovieWidth;
   g := m_nMovieHeight;
   h := nFPS;
   fs.Write(f, sizeof(integer));
   fs.Write(g, sizeof(integer));
   //
   fs.Write(h, sizeof(integer));
   //
   fs.Write(m_bgColor, sizeof(m_bgColor));
   for f := 0 to m_olLayers.Count-1 do
   begin
      pLayer := m_olLayers.Items[f];

      i := length(pLayer^.m_strName)+1;
      fs.Write(i, sizeof(i));
      fs.Write(pLayer^.m_strName, i);

      fs.Write(pLayer^.m_nType, sizeof(pLayer^.m_nType));
      fs.Write(pLayer^.m_olFrames.Count, sizeof(pLayer^.m_olFrames.Count));

      fs.Write(pLayer^.m_olActions.Count, sizeof(pLayer^.m_olActions.Count));
      for g := 0 to pLayer^.m_olActions.Count-1 do
      begin
         pAction := pLayer^.m_olActions.Items[g];
         fs.Write(pAction^.m_nType, sizeof(pAction^.m_nType));
         fs.Write(pAction^.m_nFrameNo, sizeof(pAction^.m_nFrameNo));
         case pAction^.m_nType of
            A_JUMPTO,A_SHAKE: begin
                  fs.Write(pAction^.m_nParams[1], sizeof(pAction^.m_nParams[1]));
                  fs.Write(pAction^.m_nParams[2], sizeof(pAction^.m_nParams[2]));
                  fs.Write(pAction^.m_nParams[3], sizeof(pAction^.m_nParams[3]));
               end;
            A_LOADNEW: begin
                  misc := length(pAction^.m_strParam)+1;
                  fs.Write(misc, sizeof(misc));
                  fs.Write(pAction^.m_strParam, misc);
               end;
            A_OLD : fs.Write(pAction^.m_nParams[1], sizeof(pAction^.m_nParams[1]));
         end;
      end;

      bWritten := FALSE;
      for g := 0 to pLayer^.m_olFrames.Count-1 do
      begin
         pFrameSet := pLayer^.m_olFrames.Items[g];
         fs.Write(pFrameSet^.m_Frames.Count, sizeof(pFrameSet^.m_Frames.Count));
         for h := 0 to pFrameSet^.m_Frames.Count-1 do
         begin
            pFrame := pFrameSet^.m_Frames.Items[h];
            fs.Write(pFrame^.m_nOnion, sizeof(pFrame^.m_nOnion));
            fs.Write(pFrame^.m_FrameNo, sizeof(pFrame^.m_FrameNo));
            if (pLayer^.m_nType = O_EDITVIDEO) then
            with TEditVideoObjPtr(pFrame^.m_pObject)^ do
            begin
               i := length(TEditVideoObjPtr(pFrame^.m_pObject)^.m_strFileName)+1;
               fs.write(i, sizeof(i));
               fs.Write(TEditVideoObjPtr(pFrame^.m_pObject)^.m_strFileName, i);
               for i := 1 to 4 do
               begin
                  fs.Write(Pnt(i)^.Left, sizeof(Pnt(i)^.Left));
                  fs.Write(Pnt(i)^.Top, sizeof(Pnt(i)^.Top));
               end;
            end;
            if (pLayer^.m_nType = O_RECTANGLE) then
            with TSquareObjPtr(pFrame^.m_pObject)^ do
            begin
               for i := 1 to 4 do
               begin
                  fs.Write(Pnt(i)^.Left, sizeof(Pnt(i)^.Left));
                  fs.Write(Pnt(i)^.Top, sizeof(Pnt(i)^.Top));
               end;
               fs.Write(m_nLineWidth, sizeof(m_nLineWidth));
               fs.Write(m_InColour, sizeof(m_InColour));
               fs.Write(m_OutColour, sizeof(m_OutColour));
               //
               fs.Write(m_styleInner, sizeof(m_styleInner));
               fs.Write(m_styleOuter, sizeof(m_styleOuter));
               //
               fs.Write(m_angle, sizeof(m_angle));
               fs.Write(m_alpha, sizeof(m_alpha));
               fs.Write(m_aliased, sizeof(m_aliased));
            end;
            if (pLayer^.m_nType = O_LINE) then
            with TLineObjPtr(pFrame^.m_pObject)^ do
            begin
               for i := 1 to 2 do
               begin
                  fs.Write(Pnt(i)^.Left, sizeof(Pnt(i)^.Left));
                  fs.Write(Pnt(i)^.Top, sizeof(Pnt(i)^.Top));
               end;
               fs.Write(m_nLineWidth, sizeof(m_nLineWidth));
               fs.Write(m_Colour, sizeof(m_Colour));
               fs.Write(m_angle, sizeof(m_angle));
               fs.Write(m_alpha, sizeof(m_alpha));
               fs.Write(m_aliased, sizeof(m_aliased));
            end;
            if (pLayer^.m_nType = O_BITMAP) then
            with TBitmanPtr(pFrame^.m_pObject)^ do
            begin
               fs.Write(m_bLoadNew, sizeof(m_bLoadNew));    //used for: is TBitmap or TGPBitmap
               if not bWritten then
               begin
                  if (m_bLoadNew) then
                     SaveBitmap(TBitmanPtr(pLayer^.m_pTempObject)^.Imarge, fs)
                  else
                     SaveBitmapOld(TBitmanPtr(pLayer^.m_pTempObject)^.Imarge, fs);

                  bTrans := TRUE;
                  fs.Write(bTrans, sizeof(bTrans));
                  //
                  bWritten := TRUE;
               end;
               for i := 1 to 4 do
               begin
                  x := Pnt(i)^.Left;
                  y := Pnt(i)^.Top;
                  fs.Write(x, sizeof(x));
                  fs.Write(y, sizeof(y));
               end;
               fs.Write(m_angle, sizeof(m_angle));
               fs.Write(m_alpha, sizeof(m_alpha));
               fs.Write(m_aliased, sizeof(m_aliased));
            end;
            if (pLayer^.m_nType = O_OVAL) then
            with TOvalObjPtr(pFrame^.m_pObject)^ do
            begin
               for i := 1 to 4 do
               begin
                  fs.Write(Pnt(i)^.Left, sizeof(Pnt(i)^.Left));
                  fs.Write(Pnt(i)^.Top, sizeof(Pnt(i)^.Top));
               end;
               fs.Write(m_nLineWidth, sizeof(m_nLineWidth));
               fs.Write(m_InColour, sizeof(m_InColour));
               fs.Write(m_OutColour, sizeof(m_OutColour));
               //
               fs.Write(m_styleInner, sizeof(m_styleInner));
               fs.Write(m_styleOuter, sizeof(m_styleOuter));
               //
               fs.Write(m_angle, sizeof(m_angle));
               fs.Write(m_alpha, sizeof(m_alpha));
               fs.Write(m_aliased, sizeof(m_aliased));
            end;
            if (pLayer^.m_nType = O_EXPLODE) then
            with TExplodeObjPtr(pFrame^.m_pObject)^ do
            begin
               for i := 1 to 2 do
               begin
                  fs.Write(Pnt(i)^.Left, sizeof(Pnt(i)^.Left));
                  fs.Write(Pnt(i)^.Top, sizeof(Pnt(i)^.Top));
               end;
               fs.Write(m_nMidX, sizeof(m_nMidX));
               fs.Write(m_nMidY, sizeof(m_nMidY));
            end;
            if (pLayer^.m_nType = O_STICKMAN) then
            with TStickManPtr(pFrame^.m_pObject)^ do
            begin
               fs.Write(m_nHeadDiam, sizeof(m_nHeadDiam));
               for i := 1 to 10 do
               begin
                  fs.Write(Pnt(i)^.Left, sizeof(Pnt(i)^.Left));
                  fs.Write(Pnt(i)^.Top, sizeof(Pnt(i)^.Top));
               end;
               for i := 1 to 10 do
               begin
                  fs.Write(Wid[i], sizeof(Wid[i]));
               end;
               for i := 1 to 9 do
               begin
                  fs.Write(Lng[i], sizeof(Lng[i]));
               end;
               fs.Write(m_InColour, sizeof(m_InColour));
               fs.Write(m_OutColour, sizeof(m_OutColour));
               fs.Write(m_angle, sizeof(m_angle));
               fs.Write(m_alpha, sizeof(m_alpha));
               fs.Write(m_aliased, sizeof(m_aliased));
            end;
            if (pLayer^.m_nType = O_T2STICK) then
            begin
               if (not bWritten) then
               begin
                  TLimbListPtr(pFrame^.m_pObject)^.CopyBitmapsShallow(TLimbListPtr(pLayer^.m_pTempObject)^);
               end;

               with TLimbListPtr(pFrame^.m_pObject)^ do
               begin
                  Write(fs);
               end;

               if (not bWritten) then
               begin
                  TLimbListPtr(pFrame^.m_pObject)^.ClearBitmaps;
                  bWritten := TRUE;
               end;
            end;
            if (pLayer^.m_nType = O_SPECIALSTICK) then
            with TSpecialStickManPtr(pFrame^.m_pObject)^ do
            begin
               fs.Write(m_nDrawStyle, sizeof(m_nDrawStyle));
               fs.Write(m_nLineWidth, sizeof(m_nLineWidth));
               fs.Write(m_nHeadDiam, sizeof(m_nHeadDiam));
               fs.Write(m_styleInner, sizeof(m_styleInner));
               fs.Write(m_styleOuter, sizeof(m_styleOuter));
               for i := 1 to 14 do
               begin
                  fs.Write(Pnt(i)^.Left, sizeof(Pnt(i)^.Left));
                  fs.Write(Pnt(i)^.Top, sizeof(Pnt(i)^.Top));
               end;
               for i := 1 to 14 do
               begin
                  fs.Write(Wid[i], sizeof(Wid[i]));
               end;
               for i := 1 to 13 do
               begin
                  fs.Write(Lng[i], sizeof(Lng[i]));
               end;
               fs.Write(m_InColour, sizeof(m_InColour));
               fs.Write(m_OutColour, sizeof(m_OutColour));
               //
               bMore := FALSE;
               fs.Write(bMore, sizeof(bMore));
               //
               fs.Write(m_angle, sizeof(m_angle));
               fs.Write(m_alpha, sizeof(m_alpha));
               fs.Write(m_aliased, sizeof(m_aliased));
            end;

            if (pLayer^.m_nType = O_STICKMANBMP) then
            with TStickManBMPPtr(pFrame^.m_pObject)^ do
            begin
               if not bWritten then
               begin
                  //faceclosed
                  SaveBitmap(TStickManBMPPtr(pLayer^.m_pTempObject)^.m_FaceClosed, fs);
                  SaveBitmap(TStickManBMPPtr(pLayer^.m_pTempObject)^.m_FaceOpen, fs);
                  bWritten := TRUE;
               end;
               fs.Write(m_nHeadDiam, sizeof(m_nHeadDiam));
               for i := 1 to 10 do
               begin
                  fs.Write(Pnt(i)^.Left, sizeof(Pnt(i)^.Left));
                  fs.Write(Pnt(i)^.Top, sizeof(Pnt(i)^.Top));
               end;
               for i := 1 to 10 do
               begin
                  fs.Write(Wid[i], sizeof(Wid[i]));
               end;
               for i := 1 to 9 do
               begin
                  fs.Write(Lng[i], sizeof(Lng[i]));
               end;
               fs.Write(m_OutColour, sizeof(m_OutColour));
               fs.Write(m_bMouthOpen, sizeof(m_bMouthOpen));
               fs.Write(m_bFlipped, sizeof(m_bFlipped));
               fs.Write(m_angle, sizeof(m_angle));
               fs.Write(m_alpha, sizeof(m_alpha));
               fs.Write(m_aliased, sizeof(m_aliased));
            end;

            if (pLayer^.m_nType = O_POLY) then
            with TPolyObjPtr(pFrame^.m_pObject)^ do
            begin
               i := PntList.Count;
               fs.Write(i, sizeof(i));
               for i := 0 to PntList.Count-1 do
               begin
                  fs.Write(TLabel2Ptr(PntList.Items[i])^.Left, sizeof(TLabel2Ptr(PntList.Items[i])^.Left));
                  fs.Write(TLabel2Ptr(PntList.Items[i])^.Top, sizeof(TLabel2Ptr(PntList.Items[i])^.Top));
               end;
               fs.Write(m_nLineWidth, sizeof(m_nLineWidth));
               fs.Write(m_InColour, sizeof(m_InColour));
               fs.Write(m_OutColour, sizeof(m_OutColour));
               //
               fs.Write(m_styleInner, sizeof(m_styleInner));
               fs.Write(m_styleOuter, sizeof(m_styleOuter));
               //
               fs.Write(m_angle, sizeof(m_angle));
               fs.Write(m_alpha, sizeof(m_alpha));
               fs.Write(m_aliased, sizeof(m_aliased));
            end;
            if (pLayer^.m_nType = O_TEXT) then
            with TTextObjPtr(pFrame^.m_pObject)^ do
            begin
               for i := 1 to 4 do
               begin
                  fs.Write(Pnt(i)^.Left, sizeof(Pnt(i)^.Left));
                  fs.Write(Pnt(i)^.Top, sizeof(Pnt(i)^.Top));
               end;
               fs.Write(m_InColour, sizeof(m_InColour));
               fs.Write(m_OutColour, sizeof(m_OutColour));
               fs.Write(m_styleOuter, sizeof(m_styleOuter));
               fs.Write(m_FontStyle, sizeof(m_FontStyle));
               i := length(m_strFontName)+1;
               fs.Write(i, sizeof(i));
               fs.Write(m_strFontName, i);
               i := length(m_strCaption)+1;
               fs.Write(i, sizeof(i));
               fs.Write(m_strCaption, i);
               fs.Write(m_angle, sizeof(m_angle));
               fs.Write(m_alpha, sizeof(m_alpha));
               fs.Write(m_aliased, sizeof(m_aliased));
            end;
         end;
      end;
   end;
   fs.Free;
end;