const MAMInterface = {
        MAMGetAppId: function () {
            if(!window.AppState) return;
            var returnStr = window.AppState.appId;
            var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
            var bufferSize = lengthBytesUTF8(returnStr) + 1; 
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer;
        },

        MAMGetInitialPosition: function () {
            if(!window.AppState) return;
            var returnStr = window.AppState.initialPosition;
            var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
            var bufferSize = lengthBytesUTF8(returnStr) + 1; 
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer;
        },

        MAMGetModelLoader: function () {
            if(!window.AppState) return;
            var returnStr = window.AppState.modelLoader;
            var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
            var bufferSize = lengthBytesUTF8(returnStr) + 1; 
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer;
        },
};

mergeInto(LibraryManager.library, MAMInterface);