import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/room/room_response.dart';
import 'package:the_lions_den_mobile/model/room_type/room_type_response.dart';
import 'package:the_lions_den_mobile/pages/room/room_details.dart';
import 'package:the_lions_den_mobile/providers/room_provider.dart';
import 'package:the_lions_den_mobile/providers/room_type_provider.dart';
import 'package:the_lions_den_mobile/utils/auth_helper.dart';
import 'package:the_lions_den_mobile/utils/util.dart';
import 'package:the_lions_den_mobile/widgets/master_screen.dart';

class RoomOverview extends StatefulWidget {
  static const String routeName = "/room-overview";

  const RoomOverview({Key? key}) : super(key: key);

  @override
  State<RoomOverview> createState() => _RoomOverviewState();
}

class _RoomOverviewState extends State<RoomOverview> {
  RoomProvider? _roomProvider;
  RoomTypeProvider? _roomTypeProvider;
  List<RoomResponse>? data;
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
    var search = {
      "name": _nameSearchController.text,
      "userId": AuthHelper.user!.userId,
      "capacity": _capacitySearchController.text.isEmpty
          ? 0
          : _capacitySearchController.text,
      "price":
          _priceSearchController.text.isEmpty ? 0 : _priceSearchController.text,
      "comparator": "<=",
      "roomTypeId": selectedRoomTypeValue ?? 0,
      "IncludeRoomType": "true",
      "IncludeAmenities": "true",
      "state": "Active"
    };
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
    return MasterScreenWidget(
        selectedIndex: 0,
        title: "ROOM OVERVIEW",
        child: SafeArea(
            child: SingleChildScrollView(
          child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: _buildAll()),
        )));
  }

  List<Widget> _buildAll() {
    List<Widget> list = <Widget>[];
    list.add(_buildHeader());
    list.add(_buildRoomSearch());
    list.addAll(_buildRoomCardList());
    return list;
  }

  Widget _buildHeader() {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
      child: const Text(
        "Search rooms",
        style: TextStyle(
            color: Color.fromARGB(255, 66, 129, 160),
            fontSize: 20,
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
                      hintText: "Name",
                      prefixIcon: Icon(Icons.search),
                      // border: OutlineInputBorder(
                      //     borderRadius: BorderRadius.circular(10),
                      //     borderSide: BorderSide(color: Colors.grey))
                    ),
                    onSubmitted: (value) async {
                      var tmpData = await _roomProvider?.get({
                        "name": value,
                        "userId": AuthHelper.user!.userId,
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
                  iconSize: 10,
                  items: _buildRoomTypesDownList(),
                  value: selectedRoomTypeValue,
                  icon: Icon(Icons.bed_outlined),
                  hint: Text("Room Type"),
                  onChanged: (dynamic value) {
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
                    setState(() {
                      selectedRoomTypeValue = -1;
                    });
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
                      "userId": AuthHelper.user!.userId,
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

  List<Widget> _buildRoomCardList() {
    if (data == null) {
      return [
        const Center(
          child: CircularProgressIndicator(
            color: Colors.black,
          ),
        )
      ];
    }

    if (data!.length == 0) {
      return [
        const Center(
          child: Text(
            "No rooms available.",
          ),
        )
      ];
    }

    List<Widget> list = data!
        .map((x) => Container(
              height: 230,
              child: Padding(
                padding: const EdgeInsets.all(16.0),
                child: Card(
                  color: const Color.fromARGB(255, 98, 161, 193),
                  child: ClipRRect(
                    child: Stack(children: [
                      Column(
                        children: [
                          GestureDetector(
                            onTap: () => Navigator.pushNamed(context,
                                "${RoomDetails.routeName}/${x.roomId}"),
                            child: Container(
                              height: 115,
                              width: 500,
                              child: Image.memory(
                                dataFromBase64String(x.coverImage!),
                                fit: BoxFit.cover,
                              ),
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
                                            GestureDetector(
                                              onTap: () => Navigator.pushNamed(
                                                  context,
                                                  "${RoomDetails.routeName}/${x.roomId}"),
                                              child: Text(
                                                x.name!,
                                                textAlign: TextAlign.left,
                                                style: const TextStyle(
                                                    fontSize: 16,
                                                    fontWeight:
                                                        FontWeight.bold),
                                              ),
                                            ),
                                            isSaved(x),
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
    List<DropdownMenuItem> list = <DropdownMenuItem>[];

    list.add(DropdownMenuItem(
      child: Text("Room Type", style: TextStyle(color: Colors.black)),
      enabled: false,
      value: -1,
    ));

    list.addAll(roomTypes
        .map((x) => DropdownMenuItem(
              child: Text(x.name!, style: TextStyle(color: Colors.black)),
              value: x.roomTypeId,
            ))
        .toList());

    return list;
  }

  isSaved(RoomResponse x) {
    if (!x.isSaved!) {
      return GestureDetector(
        child: Icon(Icons.favorite_border_outlined, size: 24),
        onTap: () async {
          String response = await _roomProvider!
              .save(AuthHelper.user!.userId!, x.roomId!.toInt());

          if (response.isNotEmpty) await loadData();
        },
      );
    } else {
      return GestureDetector(
        child: Icon(Icons.favorite, size: 24),
        onTap: () async {
          var response = await _roomProvider!
              .removeSaved(AuthHelper.user!.userId!, x.roomId!.toInt());

          if (response.isNotEmpty) await loadData();
        },
      );
    }
  }
}
