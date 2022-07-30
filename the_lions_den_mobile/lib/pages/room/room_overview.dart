import 'dart:html';
import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/room/room_response.dart';
import 'package:the_lions_den_mobile/model/room_type/room_type_response.dart';
import 'package:the_lions_den_mobile/providers/room_provider.dart';
import 'package:the_lions_den_mobile/providers/room_type_provider.dart';
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
  RoomTypeProvider? _roomTypeProvider;
  List<RoomResponse> data = [];
  List<RoomTypeResponse> roomTypes = [];
  TextEditingController _nameSearchController = new TextEditingController();
  TextEditingController _priceSearchController = new TextEditingController();
  TextEditingController _capacitySearchController = new TextEditingController();
  TextEditingController _roomTypeSearchController = new TextEditingController();
  int? selectedRoomTypeValue;

  @override
  void initState() {
    super.initState();
    _roomProvider = context.read<RoomProvider>();
    _roomTypeProvider = context.read<RoomTypeProvider>();
    loadData();
    loadRoomTypes();
  }

  Future loadData() async {
    var search = {"IncludeRoomType": "true", "IncludeAmenities": "true"};
    var tempData = await _roomProvider?.get(search);
    setState(() {
      data = tempData!;
    });
  }

  Future loadRoomTypes() async {
    var _roomTypes = await _roomTypeProvider?.get(null);
    setState(() {
      roomTypes = _roomTypes!;
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
            height: MediaQuery.of(context).size.height,
            width: MediaQuery.of(context).size.width,
            child: ListView(
                // gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                //     crossAxisCount: 1,
                //     childAspectRatio: 1,
                //     crossAxisSpacing: 5,
                //     mainAxisSpacing: 5),
                scrollDirection: Axis.vertical,
                children: _buildRoomCardList2()),
          ),
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
      child: const Text(
        "Rooms",
        style: TextStyle(
            color: Color.fromARGB(255, 66, 129, 160),
            fontSize: 40,
            fontWeight: FontWeight.bold),
      ),
    );
  }

  Widget _buildRoomSearch() {
    return Column(
      children: [
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: [
            Expanded(
              child: Container(
                  padding: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
                  child: TextField(
                    controller: _nameSearchController,
                    decoration: InputDecoration(
                      hintText: "Search Rooms",
                      prefixIcon: Icon(Icons.search),
                      // border: OutlineInputBorder(
                      //     borderRadius: BorderRadius.circular(10),
                      //     borderSide: BorderSide(color: Colors.grey))
                    ),
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
            Expanded(
              child: Container(
                  padding: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
                  child: TextField(
                    controller: _priceSearchController,
                    decoration: InputDecoration(
                      hintText: "Price",
                      prefixIcon: Icon(Icons.money),
                      // border: OutlineInputBorder(
                      //   borderRadius: BorderRadius.circular(10),
                      //   borderSide: BorderSide(color: Colors.grey)
                      // )
                    ),
                    keyboardType: TextInputType.number,
                  )),
            ),
          ],
        ),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: [
            Expanded(
                child: Container(
              padding: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
              child: DropdownButton(
                  items: _buildRoomTypesDownList(),
                  value: selectedRoomTypeValue,
                  icon: Icon(Icons.bed_outlined),
                  hint: Text("Room Type"),
                  onChanged: (dynamic value) {
                    //if (mounted) {
                    setState(() {
                      selectedRoomTypeValue = value;
                    });
                  }),
            )),
            Expanded(
              child: Container(
                  padding: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
                  child: TextField(
                    controller: _capacitySearchController,
                    decoration: InputDecoration(
                      hintText: "Capacity",
                      prefixIcon: Icon(Icons.person_outlined),
                      // border: OutlineInputBorder(
                      //     borderRadius: BorderRadius.circular(10),
                      //     borderSide: BorderSide(color: Colors.grey)
                      //     )
                    ),
                    keyboardType: TextInputType.number,
                  )),
            ),
          ],
        ),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Expanded(
              child: Container(
                padding: EdgeInsets.symmetric(horizontal: 5, vertical: 5),
                child: IconButton(
                  icon: Icon(Icons.delete),
                  onPressed: () async {
                    _capacitySearchController.text = "";
                    _nameSearchController.text = "";
                    _priceSearchController.text = "";
                    selectedRoomTypeValue = -1;
                  },
                ),
              ),
            ),
            Expanded(
              child: Container(
                padding: EdgeInsets.symmetric(horizontal: 5, vertical: 5),
                child: IconButton(
                  icon: Icon(Icons.filter_list_alt),
                  onPressed: () async {
                    var tmpData = await _roomProvider?.get({
                      "name": _nameSearchController.text,
                      "capacity": _capacitySearchController.text.isEmpty
                          ? 0
                          : _capacitySearchController.text,
                      "price": _priceSearchController.text.isEmpty
                          ? 0
                          : _priceSearchController.text,
                      "comparator": "<=",
                      "roomTypeId": selectedRoomTypeValue ?? 0,
                      "IncludeRoomType": "true",
                      "IncludeAmenities": "true"
                    });
                    setState(() {
                      data = tmpData!;
                    });
                  },
                ),
              ),
            ),
          ],
        ),
      ],
    );
  }

  List<Widget> _buildRoomList2() {
    if (data.length == 0) {
      return [Text("Loading.....")];
    }

    List<Widget> list = data
        .map((x) => Container(
              child: Padding(
                padding: const EdgeInsets.all(16.0),
                child: AspectRatio(
                  aspectRatio: 1,
                  child: Card(
                    color: Colors.grey,
                    child: ClipRRect(
                      borderRadius: BorderRadius.all(Radius.circular(32.0)),
                      child: Column(
                        children: <Widget>[
                          AspectRatio(
                            aspectRatio: 1.5,
                            child: Container(
                              child: imageFromBase64String(x.coverImage!),
                            ),
                          ),
                          Expanded(
                            child: Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Column(
                                mainAxisAlignment:
                                    MainAxisAlignment.spaceEvenly,
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: <Widget>[
                                  Text(
                                    x.name!, textAlign: TextAlign.left,
                                    //style: TextStyles(context).getBoldStyle(),
                                  ),
                                  Text(
                                    x.roomTypeName!, textAlign: TextAlign.left,
                                    // Helper.getRoomText(hotelInfo.roomData!),
                                    // style:
                                    //     TextStyles(context).getRegularStyle().copyWith(
                                    //           fontWeight: FontWeight.w100,
                                    //           fontSize: 12,
                                    //           color: Theme.of(context)
                                    //               .disabledColor
                                    //               .withOpacity(0.6),
                                    //         ),
                                  ),
                                  Text("${formatNumber(x.price)}\$",
                                      textAlign: TextAlign.right
                                      // Helper.getRoomText(hotelInfo.roomData!),
                                      // style:
                                      //     TextStyles(context).getRegularStyle().copyWith(
                                      //           fontWeight: FontWeight.w100,
                                      //           fontSize: 12,
                                      //           color: Theme.of(context)
                                      //               .disabledColor
                                      //               .withOpacity(0.6),
                                      //         ),
                                      ),
                                ],
                              ),
                            ),
                          )
                        ],
                      ),
                    ),
                  ),
                ),
              ),
            ))
        .cast<Widget>()
        .toList();

    return list;
  }

  List<Widget> _buildRoomCardList2() {
    if (data.length == 0) {
      return [const Text("Loading.....")];
    }

    List<Widget> list = data
        .map((x) => Container(
              height: 225,
              child: Padding(
                padding: const EdgeInsets.all(16.0),
                child: Card(
                  color: const Color.fromARGB(255, 98, 161, 193),
                  child: ClipRRect(
                    borderRadius: BorderRadius.circular(16),
                    child: Stack(children: [
                      Column(
                        children: [
                          Container(
                            height: 115,
                            width: 500,
                            child: Image.memory(
                              dataFromBase64String(x.coverImage!),
                              fit: BoxFit.cover,
                            ),
                          ),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.center,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Expanded(
                                  child: Container(
                                child: Padding(
                                    padding: const EdgeInsets.only(
                                        top: 8, bottom: 8, right: 8, left: 16),
                                    child: Column(
                                      mainAxisAlignment:
                                          MainAxisAlignment.center,
                                      crossAxisAlignment:
                                          CrossAxisAlignment.start,
                                      children: [
                                        Row(
                                          mainAxisAlignment:
                                              MainAxisAlignment.spaceBetween,
                                          crossAxisAlignment:
                                              CrossAxisAlignment.center,
                                          children: [
                                            Text(
                                              x.name!,
                                              textAlign: TextAlign.left,
                                              style: const TextStyle(
                                                  fontSize: 16,
                                                  fontWeight: FontWeight.bold),
                                            ),
                                            const Icon(
                                              Icons.favorite_border_outlined,
                                              size: 24,
                                            ),
                                          ],
                                        ),
                                        Row(
                                          mainAxisAlignment:
                                              MainAxisAlignment.start,
                                          crossAxisAlignment:
                                              CrossAxisAlignment.center,
                                          children: [
                                            Text(
                                              x.roomTypeName!,
                                              textAlign: TextAlign.left,
                                              style: const TextStyle(
                                                  fontSize: 12,
                                                  fontWeight: FontWeight.w400),
                                            ),
                                          ],
                                        ),
                                        Row(
                                          mainAxisAlignment:
                                              MainAxisAlignment.end,
                                          crossAxisAlignment:
                                              CrossAxisAlignment.center,
                                          children: [
                                            Text(
                                              "${x.price!}\$ /per night",
                                              textAlign: TextAlign.left,
                                              style: const TextStyle(
                                                  fontSize: 14,
                                                  fontWeight: FontWeight.bold),
                                            ),
                                          ],
                                        ),
                                      ],
                                    )),
                              ))
                            ],
                          )
                        ],
                      )
                    ]),
                  ),
                ),
              ),
            ))
        .cast<Widget>()
        .toList();

    return list;
  }

  List<DropdownMenuItem> _buildRoomTypesDownList() {
    if (roomTypes.isEmpty) {
      return [];
    }
    List<DropdownMenuItem> list = roomTypes
        .map((x) => DropdownMenuItem(
              child: Text(x.name!, style: TextStyle(color: Colors.black)),
              value: x.roomTypeId,
            ))
        .toList();
    return list;
  }
}
