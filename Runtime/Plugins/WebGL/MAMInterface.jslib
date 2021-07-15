const MAMInterface = {
        InternalGetAppId: function () {
            if(!window.AppState) return;
            var returnStr = window.AppState.appId;
            var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
            var bufferSize = lengthBytesUTF8(returnStr) + 1; 
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer;
        }

        InternalGetInitialPosition: function () {
            if(!window.AppState) return;
            var returnStr = window.AppState.initialPosition;
            var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
            var bufferSize = lengthBytesUTF8(returnStr) + 1; 
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer;
        }

        InternalGetModelLoader: function () {
            if(!window.AppState) return;
            var returnStr = window.AppState.modelLoader;
            var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
            var bufferSize = lengthBytesUTF8(returnStr) + 1; 
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer;
        }
};

mergeInto(LibraryManager.library, MAMInterface);