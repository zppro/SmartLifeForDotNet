DECLARE @g geometry;
SET @g = geometry::STGeomFromText('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0),(2 2, 2 1, 1 1, 1 2, 2 2))', 0);
SELECT @g.STArea();
SELECT @g.STCentroid().ToString();
go
DECLARE @g geometry;
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 3)', 0);
SELECT @g.STAsBinary();
SELECT @g.STAsText();

DECLARE @g geometry;
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 0 2, 2 0)', 0);
SELECT @g.STBoundary().ToString();

DECLARE @g geometry;
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 4 0)', 0);
SELECT @g.STBuffer(1).ToString();

POLYGON ((0 -1, 4 -1, 4.0514599084854126 -0.99869883060455322, 4.1022441387176514 -0.99483710527420044, 4.1522901058197021 -0.98847776651382446, 4.2015348672866821 -0.97968357801437378, 4.249915599822998 -0.96851736307144165, 4.2973693609237671 -0.95504200458526611, 4.3438335657119751 -0.93932026624679565, 4.38924515247345 -0.92141503095626831, 4.4335412979125977 -0.90138912200927734, 4.4766594171524048 -0.87930542230606079, 4.5185363292694092 -0.85522663593292236, 4.5591094493865967 -0.82921570539474487, 4.598315954208374 -0.80133545398712158, 4.6360929012298584 -0.771648645401001, 4.672377347946167 -0.74021816253662109, 4.7071068286895752 -0.70710676908493042, 4.7402181625366211 -0.67237740755081177, 4.771648645401001 -0.6360929012298584, 4.8013354539871216 -0.598315954208374, 4.8292156457901 -0.55910950899124146, 4.8552266359329224 -0.518536388874054, 4.879305362701416 -0.47665941715240479, 4.9013891220092773 -0.43354135751724243, 4.9214150905609131 -0.38924515247344971, 4.9393202066421509 -0.3438335657119751, 4.9550420045852661 -0.29736942052841187, 4.9685173034667969 -0.24991559982299805, 4.979683518409729 -0.20153486728668213, 4.98847770690918 -0.15229016542434692, 4.9948371648788452 -0.10224419832229614, 4.9986988306045532 -0.051459848880767822, 5 0, 4.9986988306045532 0.051459848880767822, 4.9948371648788452 0.10224419832229614, 4.98847770690918 0.15229016542434692, 4.979683518409729 0.20153486728668213, 4.9685173034667969 0.24991559982299805, 4.9550420045852661 0.29736942052841187, 4.9393202066421509 0.3438335657119751, 4.9214150905609131 0.38924515247344971, 4.9013891220092773 0.43354135751724243, 4.879305362701416 0.47665941715240479, 4.8552266359329224 0.518536388874054, 4.8292156457901 0.55910950899124146, 4.8013354539871216 0.598315954208374, 4.771648645401001 0.6360929012298584, 4.7402181625366211 0.67237740755081177, 4.7071068286895752 0.70710676908493042, 4.672377347946167 0.74021816253662109, 4.6360929012298584 0.771648645401001, 4.598315954208374 0.80133545398712158, 4.5591094493865967 0.82921570539474487, 4.5185363292694092 0.85522663593292236, 4.4766594171524048 0.87930542230606079, 4.4335412979125977 0.90138912200927734, 4.38924515247345 0.92141503095626831, 4.3438335657119751 0.93932026624679565, 4.2973693609237671 0.95504200458526611, 4.249915599822998 0.96851736307144165, 4.2015348672866821 0.97968357801437378, 4.1522901058197021 0.98847776651382446, 4.1022441387176514 0.99483710527420044, 4.0514599084854126 0.99869883060455322, 4 1, 0 1, -0.0514599084854126 0.99869883060455322, -0.10224413871765137 0.99483710527420044, -0.15229010581970215 0.98847776651382446, -0.20153486728668213 0.97968357801437378, -0.24991559982299805 0.96851736307144165, -0.29736936092376709 0.95504200458526611, -0.3438335657119751 0.93932026624679565, -0.38924515247344971 0.92141503095626831, -0.43354129791259766 0.90138912200927734, -0.47665941715240479 0.87930542230606079, -0.51853632926940918 0.85522663593292236, -0.55910944938659668 0.82921570539474487, -0.598315954208374 0.80133545398712158, -0.6360929012298584 0.771648645401001, -0.672377347946167 0.74021816253662109, -0.70710670948028564 0.70710676908493042, -0.74021816253662109 0.67237740755081177, -0.771648645401001 0.6360929012298584, -0.80133545398712158 0.598315954208374, -0.8292156457901001 0.55910950899124146, -0.85522663593292236 0.518536388874054, -0.879305362701416 0.47665941715240479, -0.90138912200927734 0.43354135751724243, -0.92141509056091309 0.38924515247344971, -0.93932020664215088 0.3438335657119751, -0.95504200458526611 0.29736942052841187, -0.96851730346679688 0.24991559982299805, -0.979683518409729 0.20153486728668213, -0.98847770690917969 0.15229016542434692, -0.99483716487884521 0.10224419832229614, -0.99869883060455322 0.051459848880767822, -1 0, -0.99869883060455322 -0.051459848880767822, -0.99483716487884521 -0.10224419832229614, -0.98847770690917969 -0.15229016542434692, -0.979683518409729 -0.20153486728668213, -0.96851730346679688 -0.24991559982299805, -0.95504200458526611 -0.29736942052841187, -0.93932020664215088 -0.3438335657119751, -0.92141509056091309 -0.38924515247344971, -0.90138912200927734 -0.43354135751724243, -0.879305362701416 -0.47665941715240479, -0.85522663593292236 -0.518536388874054, -0.8292156457901001 -0.55910950899124146, -0.80133545398712158 -0.598315954208374, -0.771648645401001 -0.6360929012298584, -0.74021816253662109 -0.67237740755081177, -0.70710670948028564 -0.70710676908493042, -0.672377347946167 -0.74021816253662109, -0.6360929012298584 -0.771648645401001, -0.598315954208374 -0.80133545398712158, -0.55910944938659668 -0.82921570539474487, -0.51853632926940918 -0.85522663593292236, -0.47665941715240479 -0.87930542230606079, -0.43354129791259766 -0.90138912200927734, -0.38924515247344971 -0.92141503095626831, -0.3438335657119751 -0.93932026624679565, -0.29736936092376709 -0.95504200458526611, -0.24991559982299805 -0.96851736307144165, -0.20153486728668213 -0.97968357801437378, -0.15229010581970215 -0.98847776651382446, -0.10224413871765137 -0.99483710527420044, -0.0514599084854126 -0.99869883060455322, 0 -1))
