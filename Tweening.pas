procedure TJoint.Tween(pStart, pEnd : TJoint; sPercent : single);
var
   f : integer;
   pNew : TJoint;
   r,g,b : array[1..3] of byte;
begin
   Clear();

   m_pData := pStart.m_pData;
   m_nState := pStart.m_nState;
   m_nColour := pStart.m_nColour;
   m_nInColour := pStart.m_nInColour;
   m_bFill := pStart.m_bFill;
   m_nIndex := pStart.m_nIndex;
   m_sAngleToParent := pStart.m_sAngleToParent;
   m_nBMPXoffs := round(pStart.m_nBMPXoffs + (( pEnd.m_nBMPXoffs - pStart.m_nBMPXoffs ) * sPercent));
   m_nBMPYoffs := round(pStart.m_nBMPYoffs + (( pEnd.m_nBMPYoffs - pStart.m_nBMPYoffs ) * sPercent));
   m_sBitmapRotation := pStart.m_sBitmapRotation + (( pEnd.m_sBitmapRotation - pStart.m_sBitmapRotation ) * sPercent);
   m_sBitmapRotation := pStart.m_sBitmapRotation + (( pEnd.m_sBitmapRotation - pStart.m_sBitmapRotation ) * sPercent);
   m_nBitmapAlpha := round(pStart.m_nBitmapAlpha + (( pEnd.m_nBitmapAlpha - pStart.m_nBitmapAlpha ) * sPercent));
   m_bShowLine := pStart.m_bShowLine;
   m_nDrawAs := pStart.m_nDrawAs;
   m_nDrawWidth := round(pStart.m_nDrawWidth + (( pEnd.m_nDrawWidth - pStart.m_nDrawWidth ) * sPercent));
   m_nBitmap := pStart.m_nBitmap;

   m_nX := round(pStart.m_nX + (( pEnd.m_nX - pStart.m_nX ) * sPercent));
   m_nY := round(pStart.m_nY + (( pEnd.m_nY - pStart.m_nY ) * sPercent));
   m_nLength := round(pStart.m_nLength + (( pEnd.m_nLength - pStart.m_nLength ) * sPercent));
   m_nLineWidth := round(pStart.m_nLineWidth + (( pEnd.m_nLineWidth - pStart.m_nLineWidth ) * sPercent));

   DWORDtoRGB(pStart.m_nColour, r[1],g[1],b[1]);
   DWORDtoRGB(pEnd.m_nColour, r[2],g[2],b[2]);
   r[3] := r[1]+round(sPercent * (r[2]-r[1]));
   g[3] := g[1]+round(sPercent * (g[2]-g[1]));
   b[3] := b[1]+round(sPercent * (b[2]-b[1]));
   RGBtoDWORD(r[3],g[3],b[3], m_nColour);

   DWORDtoRGB(pStart.m_nInColour, r[1],g[1],b[1]);
   DWORDtoRGB(pEnd.m_nInColour, r[2],g[2],b[2]);
   r[3] := r[1]+round(sPercent * (r[2]-r[1]));
   g[3] := g[1]+round(sPercent * (g[2]-g[1]));
   b[3] := b[1]+round(sPercent * (b[2]-b[1]));
   RGBtoDWORD(r[3],g[3],b[3], m_nInColour);

   for f := 0 to pStart.m_olChildren.Count-1 do
   begin
      pNew := TJoint.Create;
      m_olChildren.Add(pNew);
      pNew.Tween(TJoint(pStart.m_olChildren.Items[f]), TJoint(pEnd.m_olChildren.Items[f]), sPercent);
   end;
end;