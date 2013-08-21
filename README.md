##GNIP Social Media API toolkit##

**Gnip.toolkit** is a free, open source toolkit intended to let users to quickly and easily setup and stream data from the [GNIP Social Media API](http://gnip.com). The toolkit offers two independent applications: Gnip.Ruler and Gnip.Stream. Both applications were written in C# and based on Microsoft’s .Net Framework 3.5 platform.

**Gnip.Ruler** is an application that allows users to configure GNIP data filtering rules for specific GNIP accounts and data providers. The application allows users to add rules pertaining to the “tag” field; a feature that is absent from the Web driven console. This may be useful for future integrations or for distinguishing incoming data from the Gnip.Stream application.

**Gnip.Streamer** is an easy to use application that allows users to capture sifted data either in real-time or reply stream from GNIP. The resulting captured data can be saved in a CSV (Comma Separated Values) file for further usage and analysis. The application offers a flexible UI where users can select which fields from incoming data will to be captured and saved to the target CSV file.
- Use **Live** or **Reply** to choose between retrieving data in real-time or historically from GNIP, respectively.
- **Collect N records** defines the total amount of records that will be captured by the Gnip.Stream application.
- **Save to file every N records** defines the interval at which a batch of records will be saved onto the output file.
- **Account** name is required to access the GNIP API. Account name can be found in the [Web driven console](https://console.gnip.com).
- **Username** and **Password** for you GNIP account are necessary to access the GNIP API.
- **Interval** defines within what timeframe data from a Reply stream will be captured. Note that trial accounts cannot go further than 3 days back.
- **Output** file is the target file where captured data will be saved. If the file is non-existent, the application will create it.
- **Fields Tree** reflects all the available options from the selected data source. Selected items will define which fields should be present in the output file.

**NOTE:** The current version of the Gnip.Stream application only captures data from Twitter power streams from GNIP. However, the code is designed to accommodate future enhancements so new sources can easily be added without modifying the application. 
