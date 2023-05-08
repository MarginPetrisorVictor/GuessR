SET IDENTITY_INSERT [dbo].[GuessModel] ON
/*INSERT INTO [dbo].[GuessModel] ([Id], [GuessRiddle], [GuessAnswer], [Score], [QuestionType], [ContentType], [ContentUrl]) VALUES (1, N'Îmi vine uneori să-mi fac o casă în pământ
Să nu mai ies din ea
Decât să-mi iau țigări și-atât
Și-o sticlă de coniac
Să vărs pe jos puțin din ea
Și-apoi s-o torn pe gât
Să beau in amintirea ta', N'Tzanca Uraganu', 0, N'Song', N'', N'')
INSERT INTO [dbo].[GuessModel] ([Id], [GuessRiddle], [GuessAnswer], [Score], [QuestionType], [ContentType], [ContentUrl]) VALUES (2, N'From which game is this image?', N'Metin2', 0, N'Game', N'Image', N'https://images.sftcdn.net/images/t_app-cover-l,f_auto/p/94e2158c-96d6-11e6-babe-00163ed833e7/1815551052/metin2-screenshot.jpg')
INSERT INTO [dbo].[GuessModel] ([Id], [GuessRiddle], [GuessAnswer], [Score], [QuestionType], [ContentType], [ContentUrl]) VALUES (3, N'Baby, calm down, calm down
Girl, this your body e put my heart for lockdown
For lockdown, oh, lockdown
Girl, you sweet like Fanta, Fanta
If I tell you say I love you no dey form yanga, oh, yanga
No tell me no, no, no, no, whoa-whoa-whoa-whoa
Oh-oh-oh-oh-oh-oh-oh-oh-oh-oh-oh', N'Rema', 0, N'Song', N'', N'')*/

--INSERT INTO [dbo].[GuessModel] ([Id], [GuessRiddle], [GuessAnswer], [Score], [QuestionType], [ContentType], [ContentUrl]) VALUES (4, N'From which movie is this image?', N'Lord of the Rings', 0, N'Game', N'Image', N'https://www.ocregister.com/wp-content/uploads/migration/mex/mexz3x-b781031337z.120121212153731000g1j1bjvqr.2.jpg?w=620')



--@@@@@@@@@@@@@ After recreating table @@@@@@@@@@@@@@@@@--

INSERT INTO [dbo].[GuessModel] ([Id], [GuessRiddle], [GuessAnswer], [Question], [QuestionType], [ContentType], [ContentUrl]) VALUES (5, N'To be, or not to be — that is the question:', N'Hamlet', 'Who said it in Hamlet play?', N'Book', N'', N'')
SET IDENTITY_INSERT [dbo].[GuessModel] OFF
