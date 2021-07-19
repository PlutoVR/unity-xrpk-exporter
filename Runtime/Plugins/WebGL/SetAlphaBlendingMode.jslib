mergeInto(LibraryManager.library, {

    SetAlphaBlendingMode: function() {
        console.log("Setting AR Alpha Blending Mode");
        GLctx.dontClearAlphaOnly = true;
    }
});