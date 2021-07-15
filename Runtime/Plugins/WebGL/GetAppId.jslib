var GetXRPKAppIdFromMAM = {
    
    GetXRPKAppId: function () {
        //var returnStr = window.top.location.href;
        if(!window.AppState) return;
        var returnStr = window.AppState.appId;
        var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
        var bufferSize = lengthBytesUTF8(returnStr) + 1; 
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer;
    }
};

mergeInto(LibraryManager.library, GetXRPKAppIdFromMAM);