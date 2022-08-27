import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/room/room_response.dart';
import 'package:the_lions_den_mobile/pages/room/room_details.dart';
import 'package:the_lions_den_mobile/providers/room_provider.dart';
import 'package:the_lions_den_mobile/utils/auth_helper.dart';
import 'package:the_lions_den_mobile/utils/util.dart';
import 'package:the_lions_den_mobile/widgets/master_screen.dart';
import 'package:the_lions_den_mobile/widgets/tld_appbar.dart';
import 'package:the_lions_den_mobile/widgets/tld_drawer.dart';

class SavedRooms extends StatefulWidget {
  static const String routeName = "/room-saved";
  const SavedRooms({Key? key}) : super(key: key);

  @override
  State<SavedRooms> createState() => _SavedRoomsState();
}

class _SavedRoomsState extends State<SavedRooms> {
  RoomProvider? _roomProvider;
  List<RoomResponse> data = [];

  @override
  void initState() {
    super.initState();
    _roomProvider = context.read<RoomProvider>();
    loadData();
  }

  Future loadData() async {
    var search = {
      "userId": AuthHelper.user!.userId,
      "savedOnly": "true",
      "IncludeRoomType": "true",
      "IncludeAmenities": "true",
      "state": "Active"
    };
    var tempData = await _roomProvider?.get(search);
    setState(() {
      data = tempData!;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
        selectedIndex: 2,
        title: "SAVED ROOMS",
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
    list.addAll(_buildRoomCardList());
    return list;
  }

  Widget _buildHeader() {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
      child: const Text(
        "Saved rooms",
        style: TextStyle(
            color: Color.fromARGB(255, 66, 129, 160),
            fontSize: 20,
            fontWeight: FontWeight.bold),
      ),
    );
  }

  List<Widget> _buildRoomCardList() {
    if (data.length == 0) {
      return [
        const Center(
          child: CircularProgressIndicator(
            color: Colors.black,
          ),
        )
      ];
    }

    List<Widget> list = data
        .map((x) => Container(
              height: 225,
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
