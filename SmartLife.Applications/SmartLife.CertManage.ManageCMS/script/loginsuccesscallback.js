function doRedirect(url) {
    alert(url);
    getTo(url, null, function (result2) {
        if (result2.Success) {
            var url2 = replaceWithKeys(result2.ret.RedirectUrl, { cmsSiteRoot: baseUrl });

            document.location.href = url2;
        }
        else {
            alertError(result2.ErrorMessage);
        }
    });
}