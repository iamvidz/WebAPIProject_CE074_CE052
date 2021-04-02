INSERT INTO [dbo].[Exam] ([Id], [question], [time], [teacher], [examid]) VALUES (1, N'Which hosting mechanism is supported by WCF?', N'2021-02-25 17:53:00', N'teacherdemo', 1)

INSERT INTO [dbo].[postedexam] ([Id], [question], [time], [teacher], [examid]) VALUES (1, N'Which hosting mechanism is supported by WCF?', N'2021-02-25 17:53:00', N'teacherdemo', 1)
INSERT INTO [dbo].[postedexam] ([Id], [question], [time], [teacher], [examid]) VALUES (2, N'Which hosting mechanism is supported by WCF?', N'2021-02-25 17:53:00', N'teacherdemo', 1)


INSERT INTO [dbo].[Question] ([question], [op1], [op2], [op3], [op4], [ans]) VALUES (N'Which hosting mechanism is supported by WCF?', N'IIS', N'Self Hosting', N'WAS', N'All', N'All')


INSERT INTO [dbo].[Student] ([name], [email], [role], [teacher], [passwd]) VALUES (N'studentdemo2.0', N'studentdemo@gmail.com', N'Student   ', N'teacherdemo', N'student@123')
INSERT INTO [dbo].[Student] ([name], [email], [role], [teacher], [passwd]) VALUES (N'studentdemo3.0', N'studentdem3o@gmail.com', N'student   ', N'teacherdemo', N'student@123')


INSERT INTO [dbo].[Teacher] ([name], [email]) VALUES (N'teacherdemo', N'teacherdemo@gmail.com')


INSERT INTO [dbo].[User] ([name], [username], [passwd], [role]) VALUES (N'new_teacher', N'new_teacher@gmail.com', N'teacher@123', N'teacher   ')
INSERT INTO [dbo].[User] ([name], [username], [passwd], [role]) VALUES (N'studentdemo', N'studentdemo@gmail.com', N'student@123', N'Student   ')
INSERT INTO [dbo].[User] ([name], [username], [passwd], [role]) VALUES (N'studentdemo3.0', N'studentdem3o@gmail.com', N'student@123', N'Student   ')
INSERT INTO [dbo].[User] ([name], [username], [passwd], [role]) VALUES (N'teacherdemo', N'teacherdemo@gmail.com', N'teacher@123', N'Teacher   ')
