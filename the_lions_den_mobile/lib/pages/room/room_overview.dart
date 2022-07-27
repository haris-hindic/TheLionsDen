import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/room/room_response.dart';
import 'package:the_lions_den_mobile/providers/room_provider.dart';
import 'package:the_lions_den_mobile/utils/number_formatter.dart';
import 'package:the_lions_den_mobile/utils/util.dart';

class RoomOverview extends StatefulWidget {
  static const String routeName = "/room-overview";

  const RoomOverview({Key? key}) : super(key: key);

  @override
  State<RoomOverview> createState() => _RoomOverviewState();
}

class _RoomOverviewState extends State<RoomOverview> {
  RoomProvider? _roomProvider;
  List<RoomResponse> data = [];
  TextEditingController _searchController = new TextEditingController();

  @override
  void initState() {
    super.initState();
    _roomProvider = context.read<RoomProvider>();
    loadData();
  }

  Future loadData() async {
    var search = {"IncludeRoomType": "true", "IncludeAmenities": "true"};
    var tempData = await _roomProvider?.get(search);
    setState(() {
      data = tempData!;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: SafeArea(
            child: SingleChildScrollView(
      child: Container(
        child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
          _buildHeader(),
          _buildRoomSearch(),
          Container(
              height: 400,
              child: GridView(
                gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                    crossAxisCount: 1,
                    childAspectRatio: 2 / 1,
                    crossAxisSpacing: 15,
                    mainAxisSpacing: 10),
                scrollDirection: Axis.horizontal,
                children: _buildRoomCardList(),
              ))
        ]),
      ),
    )));
  }

  List<Widget> _buildRoomCardList() {
    if (data.length == 0) {
      return [Text("Loading.....")];
    }

    List<Widget> list = data
        .map((x) => Container(
              width: 200,
              height: 200,
              child: Column(
                children: [
                  Container(
                    height: 100,
                    width: 100,
                    child: imageFromBase64String(x.coverImage!),
                  ),
                  Text(x.name ?? ""),
                  Text(formatNumber(x.price))
                ],
              ),
            ))
        .cast<Widget>()
        .toList();

    return list;
  }

  Widget _buildHeader() {
    return Container(
      padding: EdgeInsets.symmetric(horizontal: 20, vertical: 10),
      child: Text(
        "Rooms",
        style: TextStyle(
            color: Colors.grey, fontSize: 40, fontWeight: FontWeight.bold),
      ),
    );
  }

  Widget _buildRoomSearch() {
    return Column(
      children: [
        Row(
          children: [
            Expanded(
              child: Container(
                  padding: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
                  child: TextField(
                    controller: _searchController,
                    decoration: InputDecoration(
                        hintText: "Search Rooms",
                        prefixIcon: Icon(Icons.search),
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(15),
                            borderSide: BorderSide(color: Colors.grey))),
                    onSubmitted: (value) async {
                      var tmpData = await _roomProvider?.get({
                        "name": value,
                        "IncludeRoomType": "true",
                        "IncludeAmenities": "true"
                      });
                      setState(() {
                        data = tmpData!;
                      });
                    },
                  )),
            ),
            Container(
              padding: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
              child: IconButton(
                icon: Icon(Icons.filter_list_alt),
                onPressed: () async {
                  var tmpData = await _roomProvider?.get({
                    "name": _searchController.text,
                    "IncludeRoomType": "true",
                    "IncludeAmenities": "true"
                  });
                  setState(() {
                    data = tmpData!;
                  });
                },
              ),
            )
          ],
        ),
      ],
    );
  }
}
