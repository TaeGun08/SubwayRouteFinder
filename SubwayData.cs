namespace SubwayRouteFinder
{
    public readonly struct SubwaySegment
    {
        public readonly string From;

        public readonly string To;

        public readonly int Line;

        public readonly int Time;

        public SubwaySegment(string from, string to, int line, int time)
        {
            From = from;
            To = to;
            Line = line;
            Time = time;
        }
    }

    public static class SubwayData
    {
        public const int TransferPenaltySeconds = 180;

        public static readonly List<SubwaySegment> Segments = new()
        {
            // 1호선 라인
            new SubwaySegment("용산", "남영", 1, 110),
            new SubwaySegment("남영", "서울역", 1, 120),
            new SubwaySegment("서울역", "시청", 1, 120),
            new SubwaySegment("시청", "종각", 1, 100),
            new SubwaySegment("종각", "종로3가", 1, 90),
            new SubwaySegment("종로3가", "종로5가", 1, 90),
            new SubwaySegment("종로5가", "동대문", 1, 90),
            new SubwaySegment("동대문", "동묘앞", 1, 80),
            new SubwaySegment("동묘앞", "신설동", 1, 80),
            new SubwaySegment("신설동", "제기동", 1, 90),
            new SubwaySegment("제기동", "청량리", 1, 100),

            // 2호선 라인
            new SubwaySegment("당산", "합정", 2, 170),
            new SubwaySegment("합정", "홍대입구", 2, 100),
            new SubwaySegment("홍대입구", "신촌", 2, 110),
            new SubwaySegment("신촌", "이대", 2, 90),
            new SubwaySegment("이대", "아현", 2, 90),
            new SubwaySegment("아현", "충정로", 2, 90),
            new SubwaySegment("충정로", "시청", 2, 110),
            new SubwaySegment("시청", "을지로입구", 2, 90),
            new SubwaySegment("을지로입구", "을지로3가", 2, 90),
            new SubwaySegment("을지로3가", "을지로4가", 2, 80),
            new SubwaySegment("을지로4가", "동대문역사문화공원", 2, 100),
            new SubwaySegment("동대문역사문화공원", "신당", 2, 100),
            new SubwaySegment("신당", "상왕십리", 2, 100),
            new SubwaySegment("상왕십리", "왕십리", 2, 90),
            new SubwaySegment("왕십리", "한양대", 2, 100),

            // 3호선 라인
            new SubwaySegment("경복궁", "안국", 3, 100),
            new SubwaySegment("안국", "종로3가", 3, 90),
            new SubwaySegment("종로3가", "을지로3가", 3, 70),
            new SubwaySegment("을지로3가", "충무로", 3, 80),
            new SubwaySegment("충무로", "동대입구", 3, 100),
            new SubwaySegment("동대입구", "약수", 3, 90),
            new SubwaySegment("약수", "금호", 3, 90),
            new SubwaySegment("금호", "옥수", 3, 90),

            // 4호선 라인
            new SubwaySegment("이촌", "신용산", 4, 100),
            new SubwaySegment("신용산", "삼각지", 4, 90),
            new SubwaySegment("삼각지", "숙대입구", 4, 100),
            new SubwaySegment("숙대입구", "서울역", 4, 100),
            new SubwaySegment("서울역", "회현", 4, 90),
            new SubwaySegment("회현", "명동", 4, 90),
            new SubwaySegment("명동", "충무로", 4, 80),
            new SubwaySegment("충무로", "동대문역사문화공원", 4, 100),
            new SubwaySegment("동대문역사문화공원", "동대문", 4, 90),
            new SubwaySegment("동대문", "혜화", 4, 90),

            // 5호선 라인
            new SubwaySegment("마포", "공덕", 5, 100),
            new SubwaySegment("공덕", "애오개", 5, 110),
            new SubwaySegment("애오개", "충정로", 5, 100),
            new SubwaySegment("충정로", "서대문", 5, 90),
            new SubwaySegment("서대문", "광화문", 5, 120),
            new SubwaySegment("광화문", "종로3가", 5, 100),
            new SubwaySegment("종로3가", "을지로4가", 5, 90),
            new SubwaySegment("을지로4가", "동대문역사문화공원", 5, 90),
            new SubwaySegment("동대문역사문화공원", "청구", 5, 100),
            new SubwaySegment("청구", "신금호", 5, 100),
            new SubwaySegment("신금호", "행당", 5, 100),
            new SubwaySegment("행당", "왕십리", 5, 100),
            new SubwaySegment("왕십리", "마장", 5, 100),

            // 6호선 라인
            new SubwaySegment("망원", "합정", 6, 100),
            new SubwaySegment("합정", "상수", 6, 100),
            new SubwaySegment("상수", "광흥창", 6, 100),
            new SubwaySegment("광흥창", "대흥", 6, 100),
            new SubwaySegment("대흥", "공덕", 6, 110),
            new SubwaySegment("공덕", "효창공원앞", 6, 100),
            new SubwaySegment("효창공원앞", "삼각지", 6, 130),
            new SubwaySegment("삼각지", "녹사평", 6, 110),
            new SubwaySegment("녹사평", "이태원", 6, 90),
            new SubwaySegment("이태원", "한강진", 6, 100),
            new SubwaySegment("한강진", "버티고개", 6, 110),
            new SubwaySegment("버티고개", "약수", 6, 90),
            new SubwaySegment("약수", "청구", 6, 90),
            new SubwaySegment("청구", "신당", 6, 90),
            new SubwaySegment("신당", "동묘앞", 6, 100),
            new SubwaySegment("동묘앞", "창신", 6, 90),
        };
    }
}